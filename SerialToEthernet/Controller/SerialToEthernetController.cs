﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SerialToEthernet
{
    public class SerialToEthernetController
    {
        public const string Version = "1.2";

        private readonly Action<string> _logAction;
        public readonly ushort TcpPort;
       
        public SerialToEthernetController(Action<string> logAction, SerialPortConfig serialPortConfig, ushort tcpPort)
        {
            _logAction = logAction;
            SerialConfig = serialPortConfig;
            ResetSerial(SerialConfig);
            TcpPort = tcpPort;
        }

        public string Name => $"{SerialConfig.Portname}<>{TcpPort}";
        private void Log(string message) => _logAction?.Invoke(message);
        public SerialPortConfig SerialConfig { get; private set; }


        private readonly object SerialLock = new object();
        private SerialPort Serial;
        public event Action SerialStatusChanged;

        public bool SerialConnected { get; private set; }

        private readonly object TcpClientsLock = new object();
        List<TcpClientAid> TcpClients = new List<TcpClientAid>();
        public event Action TcpStatusChanged;
        public bool TcpListening { get; private set; }

        public bool Running { get; private set; }
        private void FirePortStatusChanged() => SerialStatusChanged?.Invoke();
        private void FireTcpStatusChanged() => TcpStatusChanged?.Invoke();

        internal bool Start()
        {
            Running = true;
            new Thread(() => StartTcpListener()) { Name = $"StartTcpListener{Name}", IsBackground = true }.Start();
            new Thread(() => KeepSerialOpen()) { Name = $"KeepPortOpen{Name}", IsBackground = true }.Start();
            return true;
        }
        internal void Stop()
        {
            Running = false;
            ResetSerial(null);
        }

        private void KeepSerialOpen()
        {
            SerialConnected = false;
            bool SerialConnected_Pre;
            bool MustClear = false;
            while (Running)
            {
                Thread.Sleep(50);
                MustClear = false;
                SerialConnected_Pre = SerialConnected;
                lock (SerialLock)
                {
                    try
                    {
                        if (Serial == null)
                        {
                            SerialConnected = false;
                            continue;
                        }
                        else
                        {
                            if (Serial.IsOpen)
                            {
                                if (SerialPort.GetPortNames().Contains(Serial.PortName) == false)
                                    MustClear = true;
                            }
                            else if (Serial.IsOpen == false)
                            {
                                try { Serial.Open(); } catch { }
                            }
                            SerialConnected = Serial.IsOpen;
                        }
                    }
                    catch
                    {
                        SerialConnected = false;
                    }
                }
                if (MustClear) ResetSerial(null);
                if (SerialConnected_Pre != SerialConnected) FirePortStatusChanged();
            }
        }
        private void ResetSerial(SerialPortConfig serialConfig)
        {
            lock (SerialLock)
            {
                if (serialConfig == null)
                {
                    if (Serial != null)
                    {
                        Serial.DataReceived -= OnSerialDataReceived;
                        if (SerialPort.GetPortNames().Contains(Serial.PortName))
                        {
                            if (Serial.IsOpen) try { Serial.Close(); } catch { }
                            try { Serial.Dispose(); } catch { }
                            FirePortStatusChanged();
                        }
                        else
                        {
                            new Task(() =>
                            {
                                if (Serial.IsOpen) try { Serial.Close(); } catch { }
                                FirePortStatusChanged();
                            }).Start();
                            Thread.Sleep(200);
                        }
                    }
                    Serial = null;
                }
                else
                {
                    if (Serial != null)
                    {
                        Serial.DataReceived -= OnSerialDataReceived;
                    }
                    Serial = new SerialPort
                    {
                        PortName = serialConfig.Portname,
                        BaudRate = serialConfig.BaudRate,
                        DataBits = serialConfig.DataBits,
                        Handshake = serialConfig.Handshake,
                        Parity = serialConfig.Parity,
                        StopBits = serialConfig.StopBits
                    };
                    Serial.DataReceived += OnSerialDataReceived;
                }
            }
        }

        private void StartTcpListener()
        {
            try
            {
                var tcpListener = new TcpListener(IPAddress.Parse("0.0.0.0"), TcpPort);
                tcpListener.Start();

                if (tcpListener.Server.IsBound)
                {
                    TcpListening = true;
                    FireTcpStatusChanged();
                }
                var threadListener = new Thread(() =>
                {
                    try
                    {
                        while (Running)
                        {
                            TcpClient client = tcpListener?.AcceptTcpClient();
                            Task.Run(() => Handle(client));
                        }
                    }
                    catch (Exception ex) { Log($"Listener thread error: {ex.Message}"); }
                    Log($"Listener stoped!");

                    TcpListening = false;
                    FireTcpStatusChanged();
                })
                { Name = $"Listener {Name}", IsBackground = true };

                new Thread(() =>
                {
                    try
                    {
                        while (Running)
                        {
                            Thread.Sleep(100);
                        }
                    }
                    catch { }
                    try { tcpListener?.Stop(); } catch { }
                    foreach (var item in TcpClients)
                    {
                        try { item.Client.Client.Shutdown(SocketShutdown.Both); } catch { }
                        try { item.Client.Client.Close(); } catch { }
                        try { item.Client.Close(); } catch { }
                    }
                    TcpClients.Clear();
                })
                { Name = $"Listener breaker {Name}", IsBackground = true }.Start();

                threadListener.Start();
                Log($"Start TcpListener OK");
            }
            catch (Exception ex)
            {
                Log($"Start TcpListener Error: {ex.Message}");
            }
        }

        private void Handle(TcpClient client)
        {
            var clientAid = default(TcpClientAid);
            try
            {
                clientAid = new TcpClientAid(client, OnTcpClientDataReady, Remove);
                Log($"Socket {clientAid.Name} connected.");
                lock (TcpClientsLock)
                    TcpClients.Add(clientAid);
            }
            catch
            {
                Remove(clientAid);

            }
        }

        private void Remove(TcpClientAid clientAid)
        {
            try
            {
                lock (TcpClientsLock)
                    if (TcpClients.Contains(clientAid))
                        TcpClients.Remove(clientAid);
            }
            catch { }
            Log($"Socket {clientAid.Name} disconnected.");
        }

        private void OnTcpClientDataReady(string name, byte[] bytes)
        {
            Log($"Tcp {name} RX> {BitConverter.ToString(bytes).Replace("-", " ")}");
            try
            {
                if (Serial != null) Serial.Write(bytes, 0, bytes.Length);
            }
            catch { }
        }

        private void OnSerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serial = sender as SerialPort;
            byte[] bytes = new byte[serial.BytesToRead];
            serial.Read(bytes, 0, bytes.Length);
            Task.Run(() => OnSerialDataReady(bytes));
        }

        private void OnSerialDataReady(byte[] bytes)
        {
            Log($"Serial RX> {BitConverter.ToString(bytes).Replace("-", " ")}");
            foreach (var client in TcpClients)
            {
                try
                {
                    Log($"Tcp {client.Name} TX> {BitConverter.ToString(bytes).Replace("-", " ")}");
                    client.Send(bytes);
                }
                catch { }
            }
        }
    }
}