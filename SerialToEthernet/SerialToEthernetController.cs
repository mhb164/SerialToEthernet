using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class SerialPortConfig
{
    public string Portname { get; set; }
    public int BaudRate { get; set; } = 9600;
    public int DataBits { get; set; } = 8;
    public Handshake Handshake { get; set; } = Handshake.None;
    public Parity Parity { get; set; } = Parity.None;
    public StopBits StopBits { get; set; } = StopBits.Two;


}
public class SerialToEthernetController
{
    public SerialToEthernetController(Action<string> logAction, SerialPortConfig serialPortConfig, ushort tcpPort)
    {
        LogAction = logAction;
        SerialConfig = serialPortConfig;
        ChangePort(SerialConfig);
        TcpPort = tcpPort;
    }

    Action<string> LogAction; private void Log(string message) => LogAction?.Invoke(message);
    public SerialPortConfig SerialConfig { get; private set; }
    public ushort TcpPort { get; private set; }


    private object SerialLock = new object();
    private SerialPort Serial;
    public event EventHandler SerialStatusChanged;
    public bool SerialConnected { get; private set; }

    public bool Running { get; private set; }
    private void FirePortStatusChanged() => SerialStatusChanged?.Invoke(null, EventArgs.Empty);

    internal bool Start()
    {
        Running = true;
        new Thread(() => KeepPortOpen()) { Name = "KeepPortOpen", IsBackground = true }.Start();

        return true;
    }
    internal void Stop()
    {
        Running = false;
        ChangePort(null);

    }

    private void KeepPortOpen()
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
            if (MustClear) ChangePort(null);
            if (SerialConnected_Pre != SerialConnected) FirePortStatusChanged();
        }
    }
    private void ChangePort(SerialPortConfig serialConfig)
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
    }
}
