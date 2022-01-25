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

        SerialToEthernetController controller;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ScanPorts();
        }
        private void StartStopButton_Click(object sender, EventArgs e) => StartStop();

        #region Methods
        public void InvokeIfNecessary(MethodInvoker action) { if (InvokeRequired) BeginInvoke(action); else { action(); } }

        private void ScanPorts()
        {
            PortsSelector.Items.Clear();
            foreach (string s in SerialPort.GetPortNames()) PortsSelector.Items.Add(s);
            StartStopButton.Enabled = PortsSelector.Items.Count > 0;
            if (PortsSelector.Items.Count > 0) PortsSelector.SelectedIndex = 0;
        }
        private void Log(string message) => InvokeIfNecessary(() => LogBox.AppendText($"{DateTime.Now:HH:mm:ss.fff>} {message}\r\n"));

        private void StartStop()
        {
            if (controller?.Running == true) Stop();
            else Start();
        }



        private void Start() => InvokeIfNecessary(() =>
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
            controller = new SerialToEthernetController(Log, serialPortConfig, (ushort)TcpPortSelector.Value);
            controller.SerialStatusChanged += OnControllerSerialStatusChanged;
            controller.TcpStatusChanged += OnControllerTcpStatusChanged;
            splitContainer1.Enabled = false;
            Task.Run(() =>
            {
                try
                {
                    controller.Start();
                }
                catch (Exception ex)
                {
                    Log($"start error: {ex.Message}");
                }

                InvokeIfNecessary(() =>
                {
                    splitContainer1.Enabled = true;
                    if (controller?.Running == true)
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


        private void Stop() => InvokeIfNecessary(() =>
        {
            if (controller == null) return;
            Log("stoping...");
            try
            {
                controller.Stop();
            }
            catch (Exception ex)
            {
                Log($"stop error: {ex.Message}");
            }
            controller = null;

            StartStopButton.Text = "Start";
            SerialPanel.Enabled = TcpPanel.Enabled = true;
            Log("stop finished");
            OnControllerSerialStatusChanged();
            OnControllerTcpStatusChanged();
        });
        #endregion


        private void OnControllerSerialStatusChanged() => this.InvokeIfNecessary(() =>
        {
            if (controller?.SerialConnected == true)
                SerialStatusLabel.BackColor = Color.Lime;
            else
                SerialStatusLabel.BackColor = Color.Salmon;
        });
        private void OnControllerTcpStatusChanged() => this.InvokeIfNecessary(() =>
        {
            if (controller?.TcpListening == true)
                TcpStatusLabel.BackColor = Color.Lime;
            else
                TcpStatusLabel.BackColor = Color.Salmon;
        });
    }
}
