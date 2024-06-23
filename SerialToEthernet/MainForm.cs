using SerialToEthernet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private SerialToEthernetController _controller;
        private bool SerialConnected => _controller?.SerialConnected == true;
        private bool TcpListening => _controller?.TcpListening == true;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ScanPorts();
        }

        private void StartStopButton_Click(object sender, EventArgs e) 
            => StartStop();

        private void ScanPorts()
        {
            PortsSelector.Items.Clear();
            foreach (string s in SerialPort.GetPortNames()) PortsSelector.Items.Add(s);
            StartStopButton.Enabled = PortsSelector.Items.Count > 0;
            if (PortsSelector.Items.Count > 0) PortsSelector.SelectedIndex = 0;
        }

        private void Log(string message)
        {
            var logLine = $"{DateTime.Now:HH:mm:ss.fff>} {message}";
            this.BeginInvokeIfNecessary(() => LogBox.AppendText($"{logLine}{Environment.NewLine}"));
        }

        private void StartStop()
        {
            if (_controller?.Running == true) Stop();
            else Start();
        }

        private void Start()
            => this.BeginInvokeIfNecessary(() =>
            {
                if (PortsSelector.SelectedIndex == -1)
                {
                    Log("serial port not selected!");
                    return;
                }

                Log("starting...");

                var serialPortConfig = new SerialPortConfig()
                {
                    Portname = PortsSelector.Items[PortsSelector.SelectedIndex].ToString(),
                };

                _controller = new SerialToEthernetController(Log, serialPortConfig, (ushort)TcpPortSelector.Value);
                _controller.SerialStatusChanged += OnControllerSerialStatusChanged;
                _controller.TcpStatusChanged += OnControllerTcpStatusChanged;
                splitContainer1.Enabled = false;

                Task.Run(() =>
                {
                    try
                    {
                        _controller.Start();
                    }
                    catch (Exception ex)
                    {
                        Log($"start error: {ex.Message}");
                    }

                    this.BeginInvokeIfNecessary(() =>
                    {
                        splitContainer1.Enabled = true;
                        if (_controller?.Running == true)
                        {
                            StartStopButton.Text = "Stop";
                            SerialPanel.Enabled = TcpPanel.Enabled = false;
                        }
                        else
                        {
                            StartStopButton.Text = "Start";
                            SerialPanel.Enabled = TcpPanel.Enabled = true;
                        }
                    });
                });
                Log("start finished");
            });


        private void Stop()
            => this.BeginInvokeIfNecessary(() =>
            {
                if (_controller == null) 
                    return;

                Log("stoping...");

                try
                {
                    _controller.Stop();
                }
                catch (Exception ex)
                {
                    Log($"stop error: {ex.Message}");
                }

                _controller = null;

                StartStopButton.Text = "Start";
                SerialPanel.Enabled = TcpPanel.Enabled = true;

                Log("stop finished");
                OnControllerSerialStatusChanged();
                OnControllerTcpStatusChanged();
            });

        private void OnControllerSerialStatusChanged()
            => this.BeginInvokeIfNecessary(() =>
            {
                if (SerialConnected)
                {
                    SerialStatusLabel.BackColor = Color.Lime;
                    return;
                }

                SerialStatusLabel.BackColor = Color.Salmon;
            });

        private void OnControllerTcpStatusChanged()
            => this.BeginInvokeIfNecessary(() =>
            {
                if (TcpListening)
                {
                    TcpStatusLabel.BackColor = Color.Lime;
                    return;
                }

                TcpStatusLabel.BackColor = Color.Salmon;
            });
    }
}
