namespace MotorProtection.UI
{
    partial class frmProtectorDetailsSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProtectorDetailsSetting));
            this.lblProtectPower = new System.Windows.Forms.Label();
            this.txtProtectPower = new System.Windows.Forms.TextBox();
            this.lblProtectPowerUnit = new System.Windows.Forms.Label();
            this.lblProtectPowerDesc = new System.Windows.Forms.Label();
            this.lblProtectMode = new System.Windows.Forms.Label();
            this.pnlProtectMode = new System.Windows.Forms.Panel();
            this.rbtnPowerMode = new System.Windows.Forms.RadioButton();
            this.rbtnCurrentMode = new System.Windows.Forms.RadioButton();
            this.lblMRI = new System.Windows.Forms.Label();
            this.txtMRI = new System.Windows.Forms.TextBox();
            this.lblAlarmGate = new System.Windows.Forms.Label();
            this.txtAlarmGate = new System.Windows.Forms.TextBox();
            this.lblAlarmGateUnit = new System.Windows.Forms.Label();
            this.lblAlarmGateDesc = new System.Windows.Forms.Label();
            this.lblStopGate = new System.Windows.Forms.Label();
            this.txtStopGate = new System.Windows.Forms.TextBox();
            this.lblStopGateUnit = new System.Windows.Forms.Label();
            this.lblStopGateDesc = new System.Windows.Forms.Label();
            this.lblMR1Mode = new System.Windows.Forms.Label();
            this.pnlMR1Mode = new System.Windows.Forms.Panel();
            this.rbtnStopOpen = new System.Windows.Forms.RadioButton();
            this.rbtnStopClose = new System.Windows.Forms.RadioButton();
            this.rbtnAlarmOpen = new System.Windows.Forms.RadioButton();
            this.rbtnAlarmClose = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtnStopOpen1 = new System.Windows.Forms.RadioButton();
            this.rbtnStopClose1 = new System.Windows.Forms.RadioButton();
            this.rbtnAlarmOpen1 = new System.Windows.Forms.RadioButton();
            this.rbtnAlarmClose1 = new System.Windows.Forms.RadioButton();
            this.lblMR2Mode = new System.Windows.Forms.Label();
            this.btnReadProtector = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMRIDesc = new System.Windows.Forms.Label();
            this.pnlProtectMode.SuspendLayout();
            this.pnlMR1Mode.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProtectPower
            // 
            this.lblProtectPower.AutoSize = true;
            this.lblProtectPower.Location = new System.Drawing.Point(13, 13);
            this.lblProtectPower.Name = "lblProtectPower";
            this.lblProtectPower.Size = new System.Drawing.Size(61, 13);
            this.lblProtectPower.TabIndex = 0;
            this.lblProtectPower.Text = "保护功率: ";
            // 
            // txtProtectPower
            // 
            this.txtProtectPower.Location = new System.Drawing.Point(73, 10);
            this.txtProtectPower.Name = "txtProtectPower";
            this.txtProtectPower.Size = new System.Drawing.Size(100, 20);
            this.txtProtectPower.TabIndex = 1;
            // 
            // lblProtectPowerUnit
            // 
            this.lblProtectPowerUnit.AutoSize = true;
            this.lblProtectPowerUnit.Location = new System.Drawing.Point(176, 14);
            this.lblProtectPowerUnit.Name = "lblProtectPowerUnit";
            this.lblProtectPowerUnit.Size = new System.Drawing.Size(25, 13);
            this.lblProtectPowerUnit.TabIndex = 2;
            this.lblProtectPowerUnit.Text = "KW";
            // 
            // lblProtectPowerDesc
            // 
            this.lblProtectPowerDesc.AutoSize = true;
            this.lblProtectPowerDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProtectPowerDesc.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblProtectPowerDesc.Location = new System.Drawing.Point(202, 13);
            this.lblProtectPowerDesc.Name = "lblProtectPowerDesc";
            this.lblProtectPowerDesc.Size = new System.Drawing.Size(68, 13);
            this.lblProtectPowerDesc.TabIndex = 3;
            this.lblProtectPowerDesc.Text = "( 0 - 500KW )";
            // 
            // lblProtectMode
            // 
            this.lblProtectMode.AutoSize = true;
            this.lblProtectMode.Location = new System.Drawing.Point(14, 46);
            this.lblProtectMode.Name = "lblProtectMode";
            this.lblProtectMode.Size = new System.Drawing.Size(61, 13);
            this.lblProtectMode.TabIndex = 4;
            this.lblProtectMode.Text = "保护方式: ";
            // 
            // pnlProtectMode
            // 
            this.pnlProtectMode.Controls.Add(this.rbtnPowerMode);
            this.pnlProtectMode.Controls.Add(this.rbtnCurrentMode);
            this.pnlProtectMode.Location = new System.Drawing.Point(70, 36);
            this.pnlProtectMode.Name = "pnlProtectMode";
            this.pnlProtectMode.Size = new System.Drawing.Size(200, 34);
            this.pnlProtectMode.TabIndex = 5;
            // 
            // rbtnPowerMode
            // 
            this.rbtnPowerMode.AutoSize = true;
            this.rbtnPowerMode.Location = new System.Drawing.Point(60, 8);
            this.rbtnPowerMode.Name = "rbtnPowerMode";
            this.rbtnPowerMode.Size = new System.Drawing.Size(49, 17);
            this.rbtnPowerMode.TabIndex = 1;
            this.rbtnPowerMode.Text = "功率";
            this.rbtnPowerMode.UseVisualStyleBackColor = true;
            // 
            // rbtnCurrentMode
            // 
            this.rbtnCurrentMode.AutoSize = true;
            this.rbtnCurrentMode.Checked = true;
            this.rbtnCurrentMode.Location = new System.Drawing.Point(4, 8);
            this.rbtnCurrentMode.Name = "rbtnCurrentMode";
            this.rbtnCurrentMode.Size = new System.Drawing.Size(49, 17);
            this.rbtnCurrentMode.TabIndex = 0;
            this.rbtnCurrentMode.TabStop = true;
            this.rbtnCurrentMode.Text = "电流";
            this.rbtnCurrentMode.UseVisualStyleBackColor = true;
            // 
            // lblMRI
            // 
            this.lblMRI.AutoSize = true;
            this.lblMRI.Location = new System.Drawing.Point(15, 81);
            this.lblMRI.Name = "lblMRI";
            this.lblMRI.Size = new System.Drawing.Size(61, 13);
            this.lblMRI.TabIndex = 6;
            this.lblMRI.Text = "互感变比: ";
            // 
            // txtMRI
            // 
            this.txtMRI.Location = new System.Drawing.Point(74, 78);
            this.txtMRI.Name = "txtMRI";
            this.txtMRI.Size = new System.Drawing.Size(49, 20);
            this.txtMRI.TabIndex = 7;
            this.txtMRI.Text = "0";
            // 
            // lblAlarmGate
            // 
            this.lblAlarmGate.AutoSize = true;
            this.lblAlarmGate.Location = new System.Drawing.Point(14, 114);
            this.lblAlarmGate.Name = "lblAlarmGate";
            this.lblAlarmGate.Size = new System.Drawing.Size(61, 13);
            this.lblAlarmGate.TabIndex = 8;
            this.lblAlarmGate.Text = "报警阈值: ";
            // 
            // txtAlarmGate
            // 
            this.txtAlarmGate.Location = new System.Drawing.Point(74, 111);
            this.txtAlarmGate.Name = "txtAlarmGate";
            this.txtAlarmGate.Size = new System.Drawing.Size(100, 20);
            this.txtAlarmGate.TabIndex = 9;
            // 
            // lblAlarmGateUnit
            // 
            this.lblAlarmGateUnit.AutoSize = true;
            this.lblAlarmGateUnit.Location = new System.Drawing.Point(177, 114);
            this.lblAlarmGateUnit.Name = "lblAlarmGateUnit";
            this.lblAlarmGateUnit.Size = new System.Drawing.Size(15, 13);
            this.lblAlarmGateUnit.TabIndex = 10;
            this.lblAlarmGateUnit.Text = "%";
            // 
            // lblAlarmGateDesc
            // 
            this.lblAlarmGateDesc.AutoSize = true;
            this.lblAlarmGateDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAlarmGateDesc.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblAlarmGateDesc.Location = new System.Drawing.Point(205, 114);
            this.lblAlarmGateDesc.Name = "lblAlarmGateDesc";
            this.lblAlarmGateDesc.Size = new System.Drawing.Size(52, 13);
            this.lblAlarmGateDesc.TabIndex = 11;
            this.lblAlarmGateDesc.Text = "( 1 - 100 )";
            // 
            // lblStopGate
            // 
            this.lblStopGate.AutoSize = true;
            this.lblStopGate.Location = new System.Drawing.Point(14, 146);
            this.lblStopGate.Name = "lblStopGate";
            this.lblStopGate.Size = new System.Drawing.Size(61, 13);
            this.lblStopGate.TabIndex = 12;
            this.lblStopGate.Text = "停机阈值: ";
            // 
            // txtStopGate
            // 
            this.txtStopGate.Location = new System.Drawing.Point(74, 143);
            this.txtStopGate.Name = "txtStopGate";
            this.txtStopGate.Size = new System.Drawing.Size(100, 20);
            this.txtStopGate.TabIndex = 13;
            // 
            // lblStopGateUnit
            // 
            this.lblStopGateUnit.AutoSize = true;
            this.lblStopGateUnit.Location = new System.Drawing.Point(176, 147);
            this.lblStopGateUnit.Name = "lblStopGateUnit";
            this.lblStopGateUnit.Size = new System.Drawing.Size(15, 13);
            this.lblStopGateUnit.TabIndex = 14;
            this.lblStopGateUnit.Text = "%";
            // 
            // lblStopGateDesc
            // 
            this.lblStopGateDesc.AutoSize = true;
            this.lblStopGateDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStopGateDesc.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblStopGateDesc.Location = new System.Drawing.Point(205, 146);
            this.lblStopGateDesc.Name = "lblStopGateDesc";
            this.lblStopGateDesc.Size = new System.Drawing.Size(52, 13);
            this.lblStopGateDesc.TabIndex = 15;
            this.lblStopGateDesc.Text = "( 1 - 100 )";
            // 
            // lblMR1Mode
            // 
            this.lblMR1Mode.AutoSize = true;
            this.lblMR1Mode.Location = new System.Drawing.Point(16, 181);
            this.lblMR1Mode.Name = "lblMR1Mode";
            this.lblMR1Mode.Size = new System.Drawing.Size(110, 13);
            this.lblMR1Mode.TabIndex = 16;
            this.lblMR1Mode.Text = "继电器#1保护方式: ";
            // 
            // pnlMR1Mode
            // 
            this.pnlMR1Mode.Controls.Add(this.rbtnStopOpen);
            this.pnlMR1Mode.Controls.Add(this.rbtnStopClose);
            this.pnlMR1Mode.Controls.Add(this.rbtnAlarmOpen);
            this.pnlMR1Mode.Controls.Add(this.rbtnAlarmClose);
            this.pnlMR1Mode.Location = new System.Drawing.Point(12, 207);
            this.pnlMR1Mode.Name = "pnlMR1Mode";
            this.pnlMR1Mode.Size = new System.Drawing.Size(258, 31);
            this.pnlMR1Mode.TabIndex = 17;
            // 
            // rbtnStopOpen
            // 
            this.rbtnStopOpen.AutoSize = true;
            this.rbtnStopOpen.Location = new System.Drawing.Point(198, 7);
            this.rbtnStopOpen.Name = "rbtnStopOpen";
            this.rbtnStopOpen.Size = new System.Drawing.Size(61, 17);
            this.rbtnStopOpen.TabIndex = 3;
            this.rbtnStopOpen.TabStop = true;
            this.rbtnStopOpen.Text = "停机开";
            this.rbtnStopOpen.UseVisualStyleBackColor = true;
            // 
            // rbtnStopClose
            // 
            this.rbtnStopClose.AutoSize = true;
            this.rbtnStopClose.Location = new System.Drawing.Point(134, 7);
            this.rbtnStopClose.Name = "rbtnStopClose";
            this.rbtnStopClose.Size = new System.Drawing.Size(61, 17);
            this.rbtnStopClose.TabIndex = 2;
            this.rbtnStopClose.TabStop = true;
            this.rbtnStopClose.Text = "停机闭";
            this.rbtnStopClose.UseVisualStyleBackColor = true;
            // 
            // rbtnAlarmOpen
            // 
            this.rbtnAlarmOpen.AutoSize = true;
            this.rbtnAlarmOpen.Location = new System.Drawing.Point(70, 7);
            this.rbtnAlarmOpen.Name = "rbtnAlarmOpen";
            this.rbtnAlarmOpen.Size = new System.Drawing.Size(61, 17);
            this.rbtnAlarmOpen.TabIndex = 1;
            this.rbtnAlarmOpen.TabStop = true;
            this.rbtnAlarmOpen.Text = "报警开";
            this.rbtnAlarmOpen.UseVisualStyleBackColor = true;
            // 
            // rbtnAlarmClose
            // 
            this.rbtnAlarmClose.AutoSize = true;
            this.rbtnAlarmClose.Checked = true;
            this.rbtnAlarmClose.Location = new System.Drawing.Point(7, 7);
            this.rbtnAlarmClose.Name = "rbtnAlarmClose";
            this.rbtnAlarmClose.Size = new System.Drawing.Size(61, 17);
            this.rbtnAlarmClose.TabIndex = 0;
            this.rbtnAlarmClose.TabStop = true;
            this.rbtnAlarmClose.Text = "报警闭";
            this.rbtnAlarmClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtnStopOpen1);
            this.panel1.Controls.Add(this.rbtnStopClose1);
            this.panel1.Controls.Add(this.rbtnAlarmOpen1);
            this.panel1.Controls.Add(this.rbtnAlarmClose1);
            this.panel1.Location = new System.Drawing.Point(13, 282);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 31);
            this.panel1.TabIndex = 19;
            // 
            // rbtnStopOpen1
            // 
            this.rbtnStopOpen1.AutoSize = true;
            this.rbtnStopOpen1.Checked = true;
            this.rbtnStopOpen1.Location = new System.Drawing.Point(198, 7);
            this.rbtnStopOpen1.Name = "rbtnStopOpen1";
            this.rbtnStopOpen1.Size = new System.Drawing.Size(61, 17);
            this.rbtnStopOpen1.TabIndex = 3;
            this.rbtnStopOpen1.TabStop = true;
            this.rbtnStopOpen1.Text = "停机开";
            this.rbtnStopOpen1.UseVisualStyleBackColor = true;
            // 
            // rbtnStopClose1
            // 
            this.rbtnStopClose1.AutoSize = true;
            this.rbtnStopClose1.Location = new System.Drawing.Point(134, 7);
            this.rbtnStopClose1.Name = "rbtnStopClose1";
            this.rbtnStopClose1.Size = new System.Drawing.Size(61, 17);
            this.rbtnStopClose1.TabIndex = 2;
            this.rbtnStopClose1.Text = "停机闭";
            this.rbtnStopClose1.UseVisualStyleBackColor = true;
            // 
            // rbtnAlarmOpen1
            // 
            this.rbtnAlarmOpen1.AutoSize = true;
            this.rbtnAlarmOpen1.Location = new System.Drawing.Point(70, 7);
            this.rbtnAlarmOpen1.Name = "rbtnAlarmOpen1";
            this.rbtnAlarmOpen1.Size = new System.Drawing.Size(61, 17);
            this.rbtnAlarmOpen1.TabIndex = 1;
            this.rbtnAlarmOpen1.Text = "报警开";
            this.rbtnAlarmOpen1.UseVisualStyleBackColor = true;
            // 
            // rbtnAlarmClose1
            // 
            this.rbtnAlarmClose1.AutoSize = true;
            this.rbtnAlarmClose1.Location = new System.Drawing.Point(7, 7);
            this.rbtnAlarmClose1.Name = "rbtnAlarmClose1";
            this.rbtnAlarmClose1.Size = new System.Drawing.Size(61, 17);
            this.rbtnAlarmClose1.TabIndex = 0;
            this.rbtnAlarmClose1.Text = "报警闭";
            this.rbtnAlarmClose1.UseVisualStyleBackColor = true;
            // 
            // lblMR2Mode
            // 
            this.lblMR2Mode.AutoSize = true;
            this.lblMR2Mode.Location = new System.Drawing.Point(17, 256);
            this.lblMR2Mode.Name = "lblMR2Mode";
            this.lblMR2Mode.Size = new System.Drawing.Size(110, 13);
            this.lblMR2Mode.TabIndex = 18;
            this.lblMR2Mode.Text = "继电器#2保护方式: ";
            // 
            // btnReadProtector
            // 
            this.btnReadProtector.Location = new System.Drawing.Point(16, 328);
            this.btnReadProtector.Name = "btnReadProtector";
            this.btnReadProtector.Size = new System.Drawing.Size(124, 30);
            this.btnReadProtector.TabIndex = 20;
            this.btnReadProtector.Text = "读保护器现场设置";
            this.btnReadProtector.UseVisualStyleBackColor = true;
            this.btnReadProtector.Click += new System.EventHandler(this.btnReadProtector_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(146, 328);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(59, 30);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(208, 328);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 30);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblMRIDesc
            // 
            this.lblMRIDesc.AutoSize = true;
            this.lblMRIDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMRIDesc.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblMRIDesc.Location = new System.Drawing.Point(128, 81);
            this.lblMRIDesc.Name = "lblMRIDesc";
            this.lblMRIDesc.Size = new System.Drawing.Size(131, 13);
            this.lblMRIDesc.TabIndex = 23;
            this.lblMRIDesc.Text = "( 请填写整数, 0代表无变比 )";
            // 
            // frmProtectorDetailsSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 368);
            this.Controls.Add(this.lblMRIDesc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReadProtector);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblMR2Mode);
            this.Controls.Add(this.pnlMR1Mode);
            this.Controls.Add(this.lblMR1Mode);
            this.Controls.Add(this.lblStopGateDesc);
            this.Controls.Add(this.lblStopGateUnit);
            this.Controls.Add(this.txtStopGate);
            this.Controls.Add(this.lblStopGate);
            this.Controls.Add(this.lblAlarmGateDesc);
            this.Controls.Add(this.lblAlarmGateUnit);
            this.Controls.Add(this.txtAlarmGate);
            this.Controls.Add(this.lblAlarmGate);
            this.Controls.Add(this.txtMRI);
            this.Controls.Add(this.lblMRI);
            this.Controls.Add(this.pnlProtectMode);
            this.Controls.Add(this.lblProtectMode);
            this.Controls.Add(this.lblProtectPowerDesc);
            this.Controls.Add(this.lblProtectPowerUnit);
            this.Controls.Add(this.txtProtectPower);
            this.Controls.Add(this.lblProtectPower);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProtectorDetailsSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑保护器详细信息";
            this.Load += new System.EventHandler(this.frmProtectorDetailsSetting_Load);
            this.pnlProtectMode.ResumeLayout(false);
            this.pnlProtectMode.PerformLayout();
            this.pnlMR1Mode.ResumeLayout(false);
            this.pnlMR1Mode.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProtectPower;
        private System.Windows.Forms.TextBox txtProtectPower;
        private System.Windows.Forms.Label lblProtectPowerUnit;
        private System.Windows.Forms.Label lblProtectPowerDesc;
        private System.Windows.Forms.Label lblProtectMode;
        private System.Windows.Forms.Panel pnlProtectMode;
        private System.Windows.Forms.RadioButton rbtnCurrentMode;
        private System.Windows.Forms.RadioButton rbtnPowerMode;
        private System.Windows.Forms.Label lblMRI;
        private System.Windows.Forms.TextBox txtMRI;
        private System.Windows.Forms.Label lblAlarmGate;
        private System.Windows.Forms.TextBox txtAlarmGate;
        private System.Windows.Forms.Label lblAlarmGateUnit;
        private System.Windows.Forms.Label lblAlarmGateDesc;
        private System.Windows.Forms.Label lblStopGate;
        private System.Windows.Forms.TextBox txtStopGate;
        private System.Windows.Forms.Label lblStopGateUnit;
        private System.Windows.Forms.Label lblStopGateDesc;
        private System.Windows.Forms.Label lblMR1Mode;
        private System.Windows.Forms.Panel pnlMR1Mode;
        private System.Windows.Forms.RadioButton rbtnAlarmClose;
        private System.Windows.Forms.RadioButton rbtnAlarmOpen;
        private System.Windows.Forms.RadioButton rbtnStopClose;
        private System.Windows.Forms.RadioButton rbtnStopOpen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtnStopOpen1;
        private System.Windows.Forms.RadioButton rbtnStopClose1;
        private System.Windows.Forms.RadioButton rbtnAlarmOpen1;
        private System.Windows.Forms.RadioButton rbtnAlarmClose1;
        private System.Windows.Forms.Label lblMR2Mode;
        private System.Windows.Forms.Button btnReadProtector;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMRIDesc;
    }
}