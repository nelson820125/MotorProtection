namespace MotorProtection.UI
{
    partial class frmProtectorSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProtectorSetting));
            this.lblLineName = new System.Windows.Forms.Label();
            this.cbxLineName = new System.Windows.Forms.ComboBox();
            this.lblProtectorName = new System.Windows.Forms.Label();
            this.txtProtectorName = new System.Windows.Forms.TextBox();
            this.lblProtectorAddress = new System.Windows.Forms.Label();
            this.txtProtectorAddress = new System.Windows.Forms.TextBox();
            this.lblProtectorAddressDesc = new System.Windows.Forms.Label();
            this.lblProtectorStatus = new System.Windows.Forms.Label();
            this.pnlProtectorStatus = new System.Windows.Forms.Panel();
            this.rbtnDeactive = new System.Windows.Forms.RadioButton();
            this.rbtnActive = new System.Windows.Forms.RadioButton();
            this.btnDetailSetting = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlProtectorStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLineName
            // 
            this.lblLineName.AutoSize = true;
            this.lblLineName.Location = new System.Drawing.Point(13, 13);
            this.lblLineName.Name = "lblLineName";
            this.lblLineName.Size = new System.Drawing.Size(73, 13);
            this.lblLineName.TabIndex = 0;
            this.lblLineName.Text = "所属生产线: ";
            // 
            // cbxLineName
            // 
            this.cbxLineName.FormattingEnabled = true;
            this.cbxLineName.Location = new System.Drawing.Point(89, 10);
            this.cbxLineName.Name = "cbxLineName";
            this.cbxLineName.Size = new System.Drawing.Size(183, 21);
            this.cbxLineName.TabIndex = 1;
            // 
            // lblProtectorName
            // 
            this.lblProtectorName.AutoSize = true;
            this.lblProtectorName.Location = new System.Drawing.Point(13, 47);
            this.lblProtectorName.Name = "lblProtectorName";
            this.lblProtectorName.Size = new System.Drawing.Size(73, 13);
            this.lblProtectorName.TabIndex = 2;
            this.lblProtectorName.Text = "保护器名称: ";
            // 
            // txtProtectorName
            // 
            this.txtProtectorName.Location = new System.Drawing.Point(89, 44);
            this.txtProtectorName.Name = "txtProtectorName";
            this.txtProtectorName.Size = new System.Drawing.Size(183, 20);
            this.txtProtectorName.TabIndex = 3;
            // 
            // lblProtectorAddress
            // 
            this.lblProtectorAddress.AutoSize = true;
            this.lblProtectorAddress.Location = new System.Drawing.Point(13, 81);
            this.lblProtectorAddress.Name = "lblProtectorAddress";
            this.lblProtectorAddress.Size = new System.Drawing.Size(73, 13);
            this.lblProtectorAddress.TabIndex = 4;
            this.lblProtectorAddress.Text = "保护器地址: ";
            // 
            // txtProtectorAddress
            // 
            this.txtProtectorAddress.Location = new System.Drawing.Point(89, 78);
            this.txtProtectorAddress.Name = "txtProtectorAddress";
            this.txtProtectorAddress.Size = new System.Drawing.Size(118, 20);
            this.txtProtectorAddress.TabIndex = 5;
            // 
            // lblProtectorAddressDesc
            // 
            this.lblProtectorAddressDesc.AutoSize = true;
            this.lblProtectorAddressDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProtectorAddressDesc.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblProtectorAddressDesc.Location = new System.Drawing.Point(213, 81);
            this.lblProtectorAddressDesc.Name = "lblProtectorAddressDesc";
            this.lblProtectorAddressDesc.Size = new System.Drawing.Size(59, 13);
            this.lblProtectorAddressDesc.TabIndex = 6;
            this.lblProtectorAddressDesc.Text = "( 填写数字 )";
            // 
            // lblProtectorStatus
            // 
            this.lblProtectorStatus.AutoSize = true;
            this.lblProtectorStatus.Location = new System.Drawing.Point(13, 115);
            this.lblProtectorStatus.Name = "lblProtectorStatus";
            this.lblProtectorStatus.Size = new System.Drawing.Size(73, 13);
            this.lblProtectorStatus.TabIndex = 7;
            this.lblProtectorStatus.Text = "保护器状态: ";
            // 
            // pnlProtectorStatus
            // 
            this.pnlProtectorStatus.Controls.Add(this.rbtnDeactive);
            this.pnlProtectorStatus.Controls.Add(this.rbtnActive);
            this.pnlProtectorStatus.Location = new System.Drawing.Point(85, 105);
            this.pnlProtectorStatus.Name = "pnlProtectorStatus";
            this.pnlProtectorStatus.Size = new System.Drawing.Size(183, 36);
            this.pnlProtectorStatus.TabIndex = 8;
            // 
            // rbtnDeactive
            // 
            this.rbtnDeactive.AutoSize = true;
            this.rbtnDeactive.Location = new System.Drawing.Point(60, 10);
            this.rbtnDeactive.Name = "rbtnDeactive";
            this.rbtnDeactive.Size = new System.Drawing.Size(49, 17);
            this.rbtnDeactive.TabIndex = 1;
            this.rbtnDeactive.TabStop = true;
            this.rbtnDeactive.Text = "禁用";
            this.rbtnDeactive.UseVisualStyleBackColor = true;
            // 
            // rbtnActive
            // 
            this.rbtnActive.AutoSize = true;
            this.rbtnActive.Checked = true;
            this.rbtnActive.Location = new System.Drawing.Point(4, 10);
            this.rbtnActive.Name = "rbtnActive";
            this.rbtnActive.Size = new System.Drawing.Size(49, 17);
            this.rbtnActive.TabIndex = 0;
            this.rbtnActive.TabStop = true;
            this.rbtnActive.Text = "启用";
            this.rbtnActive.UseVisualStyleBackColor = true;
            // 
            // btnDetailSetting
            // 
            this.btnDetailSetting.Location = new System.Drawing.Point(16, 161);
            this.btnDetailSetting.Name = "btnDetailSetting";
            this.btnDetailSetting.Size = new System.Drawing.Size(85, 30);
            this.btnDetailSetting.TabIndex = 9;
            this.btnDetailSetting.Text = "详细设置 >>";
            this.btnDetailSetting.UseVisualStyleBackColor = true;
            this.btnDetailSetting.Click += new System.EventHandler(this.btnDetailSetting_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(119, 160);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(197, 160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmProtectorSetting
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 205);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDetailSetting);
            this.Controls.Add(this.pnlProtectorStatus);
            this.Controls.Add(this.lblProtectorStatus);
            this.Controls.Add(this.lblProtectorAddressDesc);
            this.Controls.Add(this.txtProtectorAddress);
            this.Controls.Add(this.lblProtectorAddress);
            this.Controls.Add(this.txtProtectorName);
            this.Controls.Add(this.lblProtectorName);
            this.Controls.Add(this.cbxLineName);
            this.Controls.Add(this.lblLineName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProtectorSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmProtectorSetting_Load);
            this.pnlProtectorStatus.ResumeLayout(false);
            this.pnlProtectorStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLineName;
        private System.Windows.Forms.ComboBox cbxLineName;
        private System.Windows.Forms.Label lblProtectorName;
        private System.Windows.Forms.TextBox txtProtectorName;
        private System.Windows.Forms.Label lblProtectorAddress;
        private System.Windows.Forms.TextBox txtProtectorAddress;
        private System.Windows.Forms.Label lblProtectorAddressDesc;
        private System.Windows.Forms.Label lblProtectorStatus;
        private System.Windows.Forms.Panel pnlProtectorStatus;
        private System.Windows.Forms.RadioButton rbtnActive;
        private System.Windows.Forms.RadioButton rbtnDeactive;
        private System.Windows.Forms.Button btnDetailSetting;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}