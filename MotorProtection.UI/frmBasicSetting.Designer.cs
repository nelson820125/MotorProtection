namespace MotorProtection.UI
{
    partial class frmBasicSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBasicSetting));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPortName = new System.Windows.Forms.Label();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.lblAttemptsDescribe = new System.Windows.Forms.Label();
            this.lblAttempts = new System.Windows.Forms.Label();
            this.txtAttempts = new System.Windows.Forms.TextBox();
            this.cbxPortName = new System.Windows.Forms.ComboBox();
            this.cbxBaydRate = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(81, 133);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 33);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(163, 133);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 33);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPortName
            // 
            this.lblPortName.AutoSize = true;
            this.lblPortName.Location = new System.Drawing.Point(12, 9);
            this.lblPortName.Name = "lblPortName";
            this.lblPortName.Size = new System.Drawing.Size(61, 13);
            this.lblPortName.TabIndex = 9;
            this.lblPortName.Text = "端        口: ";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(12, 45);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(61, 13);
            this.lblBaudRate.TabIndex = 11;
            this.lblBaudRate.Text = "波  特  率: ";
            // 
            // lblAttemptsDescribe
            // 
            this.lblAttemptsDescribe.AutoSize = true;
            this.lblAttemptsDescribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAttemptsDescribe.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblAttemptsDescribe.Location = new System.Drawing.Point(77, 108);
            this.lblAttemptsDescribe.Name = "lblAttemptsDescribe";
            this.lblAttemptsDescribe.Size = new System.Drawing.Size(123, 13);
            this.lblAttemptsDescribe.TabIndex = 15;
            this.lblAttemptsDescribe.Text = "尝试连接的次数，建议3次";
            // 
            // lblAttempts
            // 
            this.lblAttempts.AutoSize = true;
            this.lblAttempts.Location = new System.Drawing.Point(12, 83);
            this.lblAttempts.Name = "lblAttempts";
            this.lblAttempts.Size = new System.Drawing.Size(61, 13);
            this.lblAttempts.TabIndex = 13;
            this.lblAttempts.Text = "尝试次数: ";
            // 
            // txtAttempts
            // 
            this.txtAttempts.Location = new System.Drawing.Point(80, 80);
            this.txtAttempts.Name = "txtAttempts";
            this.txtAttempts.Size = new System.Drawing.Size(158, 20);
            this.txtAttempts.TabIndex = 14;
            this.txtAttempts.Text = "3";
            // 
            // cbxPortName
            // 
            this.cbxPortName.FormattingEnabled = true;
            this.cbxPortName.Location = new System.Drawing.Point(79, 6);
            this.cbxPortName.Name = "cbxPortName";
            this.cbxPortName.Size = new System.Drawing.Size(159, 21);
            this.cbxPortName.TabIndex = 10;
            // 
            // cbxBaydRate
            // 
            this.cbxBaydRate.FormattingEnabled = true;
            this.cbxBaydRate.Items.AddRange(new object[] {
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
            this.cbxBaydRate.Location = new System.Drawing.Point(79, 42);
            this.cbxBaydRate.Name = "cbxBaydRate";
            this.cbxBaydRate.Size = new System.Drawing.Size(159, 21);
            this.cbxBaydRate.TabIndex = 12;
            // 
            // frmBasicSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 178);
            this.Controls.Add(this.lblPortName);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.lblAttemptsDescribe);
            this.Controls.Add(this.lblAttempts);
            this.Controls.Add(this.txtAttempts);
            this.Controls.Add(this.cbxPortName);
            this.Controls.Add(this.cbxBaydRate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBasicSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通讯设置";
            this.Load += new System.EventHandler(this.frmBasicSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPortName;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Label lblAttemptsDescribe;
        private System.Windows.Forms.Label lblAttempts;
        private System.Windows.Forms.TextBox txtAttempts;
        private System.Windows.Forms.ComboBox cbxPortName;
        private System.Windows.Forms.ComboBox cbxBaydRate;
    }
}