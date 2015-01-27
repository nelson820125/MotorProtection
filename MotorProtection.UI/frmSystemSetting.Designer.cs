namespace MotorProtection.UI
{
    partial class frmSystemSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSystemSetting));
            this.lblAudioFormatDescription = new System.Windows.Forms.Label();
            this.btnAudioTest = new System.Windows.Forms.Button();
            this.btnOpenFileDialog = new System.Windows.Forms.Button();
            this.txtAlarmAudioPath = new System.Windows.Forms.TextBox();
            this.lblAlarmAudio = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.ofdAlarmAudio = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lblAudioFormatDescription
            // 
            this.lblAudioFormatDescription.AutoSize = true;
            this.lblAudioFormatDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAudioFormatDescription.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblAudioFormatDescription.Location = new System.Drawing.Point(80, 9);
            this.lblAudioFormatDescription.Name = "lblAudioFormatDescription";
            this.lblAudioFormatDescription.Size = new System.Drawing.Size(75, 13);
            this.lblAudioFormatDescription.TabIndex = 16;
            this.lblAudioFormatDescription.Text = "( 仅支持 .wav )";
            // 
            // btnAudioTest
            // 
            this.btnAudioTest.Image = ((System.Drawing.Image)(resources.GetObject("btnAudioTest.Image")));
            this.btnAudioTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAudioTest.Location = new System.Drawing.Point(91, 56);
            this.btnAudioTest.Name = "btnAudioTest";
            this.btnAudioTest.Size = new System.Drawing.Size(84, 28);
            this.btnAudioTest.TabIndex = 15;
            this.btnAudioTest.Text = "  测试音频";
            this.btnAudioTest.UseVisualStyleBackColor = true;
            this.btnAudioTest.Click += new System.EventHandler(this.btnAudioTest_Click);
            // 
            // btnOpenFileDialog
            // 
            this.btnOpenFileDialog.Location = new System.Drawing.Point(15, 56);
            this.btnOpenFileDialog.Name = "btnOpenFileDialog";
            this.btnOpenFileDialog.Size = new System.Drawing.Size(70, 28);
            this.btnOpenFileDialog.TabIndex = 14;
            this.btnOpenFileDialog.Text = "添加文件";
            this.btnOpenFileDialog.UseVisualStyleBackColor = true;
            this.btnOpenFileDialog.Click += new System.EventHandler(this.btnOpenFileDialog_Click);
            // 
            // txtAlarmAudioPath
            // 
            this.txtAlarmAudioPath.Enabled = false;
            this.txtAlarmAudioPath.Location = new System.Drawing.Point(15, 30);
            this.txtAlarmAudioPath.Name = "txtAlarmAudioPath";
            this.txtAlarmAudioPath.Size = new System.Drawing.Size(257, 20);
            this.txtAlarmAudioPath.TabIndex = 13;
            // 
            // lblAlarmAudio
            // 
            this.lblAlarmAudio.AutoSize = true;
            this.lblAlarmAudio.Location = new System.Drawing.Point(12, 9);
            this.lblAlarmAudio.Name = "lblAlarmAudio";
            this.lblAlarmAudio.Size = new System.Drawing.Size(73, 13);
            this.lblAlarmAudio.TabIndex = 12;
            this.lblAlarmAudio.Text = "报警提示音: ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(181, 56);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 28);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ofdAlarmAudio
            // 
            this.ofdAlarmAudio.Filter = "WAV files|*.wav";
            // 
            // frmSystemSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 97);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblAudioFormatDescription);
            this.Controls.Add(this.btnAudioTest);
            this.Controls.Add(this.btnOpenFileDialog);
            this.Controls.Add(this.txtAlarmAudioPath);
            this.Controls.Add(this.lblAlarmAudio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSystemSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.frmSystemSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAudioFormatDescription;
        private System.Windows.Forms.Button btnAudioTest;
        private System.Windows.Forms.Button btnOpenFileDialog;
        private System.Windows.Forms.TextBox txtAlarmAudioPath;
        private System.Windows.Forms.Label lblAlarmAudio;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.OpenFileDialog ofdAlarmAudio;
    }
}