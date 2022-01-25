namespace System.Windows.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TcpPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.TcpPortSelector = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SerialPanel = new System.Windows.Forms.Panel();
            this.ScanPortsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PortsSelector = new System.Windows.Forms.ComboBox();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.LogBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SerialStatusLabel = new System.Windows.Forms.Label();
            this.TcpStatusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.TcpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TcpPortSelector)).BeginInit();
            this.SerialPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TcpPanel);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.SerialPanel);
            this.splitContainer1.Panel1.Controls.Add(this.StartStopButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LogBox);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(494, 473);
            this.splitContainer1.SplitterDistance = 66;
            this.splitContainer1.TabIndex = 0;
            // 
            // TcpPanel
            // 
            this.TcpPanel.Controls.Add(this.label5);
            this.TcpPanel.Controls.Add(this.TcpPortSelector);
            this.TcpPanel.Controls.Add(this.label4);
            this.TcpPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TcpPanel.Location = new System.Drawing.Point(0, 33);
            this.TcpPanel.Name = "TcpPanel";
            this.TcpPanel.Size = new System.Drawing.Size(419, 32);
            this.TcpPanel.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Consolas", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 32);
            this.label5.TabIndex = 0;
            this.label5.Text = "Eternet";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TcpPortSelector
            // 
            this.TcpPortSelector.Location = new System.Drawing.Point(116, 5);
            this.TcpPortSelector.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.TcpPortSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TcpPortSelector.Name = "TcpPortSelector";
            this.TcpPortSelector.Size = new System.Drawing.Size(96, 23);
            this.TcpPortSelector.TabIndex = 4;
            this.TcpPortSelector.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TcpPortSelector.Value = new decimal(new int[] {
            21400,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Port:";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Consolas", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(0, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(419, 1);
            this.label2.TabIndex = 2;
            this.label2.Text = "Eternet";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SerialPanel
            // 
            this.SerialPanel.Controls.Add(this.ScanPortsButton);
            this.SerialPanel.Controls.Add(this.label1);
            this.SerialPanel.Controls.Add(this.PortsSelector);
            this.SerialPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SerialPanel.Location = new System.Drawing.Point(0, 0);
            this.SerialPanel.Name = "SerialPanel";
            this.SerialPanel.Size = new System.Drawing.Size(419, 32);
            this.SerialPanel.TabIndex = 0;
            // 
            // ScanPortsButton
            // 
            this.ScanPortsButton.AutoSize = true;
            this.ScanPortsButton.Location = new System.Drawing.Point(68, 2);
            this.ScanPortsButton.Name = "ScanPortsButton";
            this.ScanPortsButton.Size = new System.Drawing.Size(98, 28);
            this.ScanPortsButton.TabIndex = 37;
            this.ScanPortsButton.Text = "Scan Ports";
            this.ScanPortsButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PortsSelector
            // 
            this.PortsSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PortsSelector.FormattingEnabled = true;
            this.PortsSelector.Location = new System.Drawing.Point(172, 5);
            this.PortsSelector.Name = "PortsSelector";
            this.PortsSelector.Size = new System.Drawing.Size(133, 23);
            this.PortsSelector.TabIndex = 36;
            // 
            // StartStopButton
            // 
            this.StartStopButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.StartStopButton.Location = new System.Drawing.Point(419, 0);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(75, 66);
            this.StartStopButton.TabIndex = 1;
            this.StartStopButton.Text = "Start";
            this.StartStopButton.UseVisualStyleBackColor = true;
            this.StartStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // LogBox
            // 
            this.LogBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.LogBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogBox.ForeColor = System.Drawing.Color.White;
            this.LogBox.Location = new System.Drawing.Point(0, 0);
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogBox.Size = new System.Drawing.Size(494, 375);
            this.LogBox.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TcpStatusLabel);
            this.panel1.Controls.Add(this.SerialStatusLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 375);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 28);
            this.panel1.TabIndex = 1;
            // 
            // SerialStatusLabel
            // 
            this.SerialStatusLabel.BackColor = System.Drawing.Color.Salmon;
            this.SerialStatusLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SerialStatusLabel.Font = new System.Drawing.Font("Consolas", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SerialStatusLabel.Location = new System.Drawing.Point(0, 0);
            this.SerialStatusLabel.Name = "SerialStatusLabel";
            this.SerialStatusLabel.Size = new System.Drawing.Size(62, 28);
            this.SerialStatusLabel.TabIndex = 0;
            this.SerialStatusLabel.Text = "SERIAL";
            this.SerialStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TcpStatusLabel
            // 
            this.TcpStatusLabel.BackColor = System.Drawing.Color.Salmon;
            this.TcpStatusLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.TcpStatusLabel.Font = new System.Drawing.Font("Consolas", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TcpStatusLabel.Location = new System.Drawing.Point(432, 0);
            this.TcpStatusLabel.Name = "TcpStatusLabel";
            this.TcpStatusLabel.Size = new System.Drawing.Size(62, 28);
            this.TcpStatusLabel.TabIndex = 1;
            this.TcpStatusLabel.Text = "TCP";
            this.TcpStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(494, 473);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Serial To Ethernet";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.TcpPanel.ResumeLayout(false);
            this.TcpPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TcpPortSelector)).EndInit();
            this.SerialPanel.ResumeLayout(false);
            this.SerialPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private Label label1;
        private NumericUpDown TcpPortSelector;
        private Label label4;
        private Button StartStopButton;
        private Panel TcpPanel;
        private Label label5;
        private Label label2;
        private Panel SerialPanel;
        private Button ScanPortsButton;
        private ComboBox PortsSelector;
        private TextBox LogBox;
        private Panel panel1;
        private Label SerialStatusLabel;
        private Label TcpStatusLabel;
    }
}

