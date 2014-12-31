namespace ModbusTester
{
    partial class SerialPortTester
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlConfiguration = new System.Windows.Forms.Panel();
            this.cbxStopBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxDataBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxPortName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlShow = new System.Windows.Forms.Panel();
            this.txtShow = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pnlConfiguration.SuspendLayout();
            this.pnlShow.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlConfiguration
            // 
            this.pnlConfiguration.Controls.Add(this.cbxStopBits);
            this.pnlConfiguration.Controls.Add(this.label5);
            this.pnlConfiguration.Controls.Add(this.cbxDataBits);
            this.pnlConfiguration.Controls.Add(this.label4);
            this.pnlConfiguration.Controls.Add(this.cbxParity);
            this.pnlConfiguration.Controls.Add(this.label3);
            this.pnlConfiguration.Controls.Add(this.cbxBaudRate);
            this.pnlConfiguration.Controls.Add(this.label2);
            this.pnlConfiguration.Controls.Add(this.cbxPortName);
            this.pnlConfiguration.Controls.Add(this.label1);
            this.pnlConfiguration.Controls.Add(this.btnStart);
            this.pnlConfiguration.Location = new System.Drawing.Point(12, 13);
            this.pnlConfiguration.Name = "pnlConfiguration";
            this.pnlConfiguration.Size = new System.Drawing.Size(117, 346);
            this.pnlConfiguration.TabIndex = 0;
            // 
            // cbxStopBits
            // 
            this.cbxStopBits.FormattingEnabled = true;
            this.cbxStopBits.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cbxStopBits.Location = new System.Drawing.Point(5, 241);
            this.cbxStopBits.Name = "cbxStopBits";
            this.cbxStopBits.Size = new System.Drawing.Size(108, 21);
            this.cbxStopBits.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Stop Bits:";
            // 
            // cbxDataBits
            // 
            this.cbxDataBits.FormattingEnabled = true;
            this.cbxDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbxDataBits.Location = new System.Drawing.Point(5, 184);
            this.cbxDataBits.Name = "cbxDataBits";
            this.cbxDataBits.Size = new System.Drawing.Size(108, 21);
            this.cbxDataBits.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Data Bits:";
            // 
            // cbxParity
            // 
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cbxParity.Location = new System.Drawing.Point(5, 130);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(108, 21);
            this.cbxParity.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Parity:";
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200"});
            this.cbxBaudRate.Location = new System.Drawing.Point(5, 77);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(108, 21);
            this.cbxBaudRate.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Baud Rate:";
            // 
            // cbxPortName
            // 
            this.cbxPortName.FormattingEnabled = true;
            this.cbxPortName.Location = new System.Drawing.Point(6, 23);
            this.cbxPortName.Name = "cbxPortName";
            this.cbxPortName.Size = new System.Drawing.Size(108, 21);
            this.cbxPortName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(4, 319);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(110, 25);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pnlShow
            // 
            this.pnlShow.Controls.Add(this.txtShow);
            this.pnlShow.Location = new System.Drawing.Point(136, 14);
            this.pnlShow.Name = "pnlShow";
            this.pnlShow.Size = new System.Drawing.Size(516, 314);
            this.pnlShow.TabIndex = 1;
            // 
            // txtShow
            // 
            this.txtShow.Location = new System.Drawing.Point(4, 4);
            this.txtShow.Multiline = true;
            this.txtShow.Name = "txtShow";
            this.txtShow.Size = new System.Drawing.Size(509, 306);
            this.txtShow.TabIndex = 0;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(136, 335);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(412, 20);
            this.txtSend.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(555, 335);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(97, 25);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // SerialPortTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 372);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.pnlShow);
            this.Controls.Add(this.pnlConfiguration);
            this.Name = "SerialPortTester";
            this.Text = "SerialPortDebuger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SerialPortTester_FormClosing);
            this.pnlConfiguration.ResumeLayout(false);
            this.pnlConfiguration.PerformLayout();
            this.pnlShow.ResumeLayout(false);
            this.pnlShow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlConfiguration;
        private System.Windows.Forms.ComboBox cbxDataBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxParity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxPortName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel pnlShow;
        private System.Windows.Forms.TextBox txtShow;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ComboBox cbxStopBits;
        private System.Windows.Forms.Label label5;
    }
}

