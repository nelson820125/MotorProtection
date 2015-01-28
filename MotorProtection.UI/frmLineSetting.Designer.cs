namespace MotorProtection.UI
{
    partial class frmLineSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLineSetting));
            this.lblLineName = new System.Windows.Forms.Label();
            this.txtLineName = new System.Windows.Forms.TextBox();
            this.lblLineStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.rbtnDeactive = new System.Windows.Forms.RadioButton();
            this.rbtnActive = new System.Windows.Forms.RadioButton();
            this.pnlStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLineName
            // 
            this.lblLineName.AutoSize = true;
            this.lblLineName.Location = new System.Drawing.Point(13, 13);
            this.lblLineName.Name = "lblLineName";
            this.lblLineName.Size = new System.Drawing.Size(73, 13);
            this.lblLineName.TabIndex = 0;
            this.lblLineName.Text = "生产线名称: ";
            // 
            // txtLineName
            // 
            this.txtLineName.Location = new System.Drawing.Point(92, 10);
            this.txtLineName.Name = "txtLineName";
            this.txtLineName.Size = new System.Drawing.Size(180, 20);
            this.txtLineName.TabIndex = 1;
            // 
            // lblLineStatus
            // 
            this.lblLineStatus.AutoSize = true;
            this.lblLineStatus.Location = new System.Drawing.Point(13, 50);
            this.lblLineStatus.Name = "lblLineStatus";
            this.lblLineStatus.Size = new System.Drawing.Size(73, 13);
            this.lblLineStatus.TabIndex = 2;
            this.lblLineStatus.Text = "生产线状态: ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(115, 89);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 29);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(197, 89);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.rbtnDeactive);
            this.pnlStatus.Controls.Add(this.rbtnActive);
            this.pnlStatus.Location = new System.Drawing.Point(92, 37);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(180, 45);
            this.pnlStatus.TabIndex = 7;
            // 
            // rbtnDeactive
            // 
            this.rbtnDeactive.AutoSize = true;
            this.rbtnDeactive.Location = new System.Drawing.Point(61, 13);
            this.rbtnDeactive.Name = "rbtnDeactive";
            this.rbtnDeactive.Size = new System.Drawing.Size(49, 17);
            this.rbtnDeactive.TabIndex = 1;
            this.rbtnDeactive.Text = "禁用";
            this.rbtnDeactive.UseVisualStyleBackColor = true;
            // 
            // rbtnActive
            // 
            this.rbtnActive.AutoSize = true;
            this.rbtnActive.Checked = true;
            this.rbtnActive.Location = new System.Drawing.Point(4, 13);
            this.rbtnActive.Name = "rbtnActive";
            this.rbtnActive.Size = new System.Drawing.Size(49, 17);
            this.rbtnActive.TabIndex = 0;
            this.rbtnActive.TabStop = true;
            this.rbtnActive.Text = "启用";
            this.rbtnActive.UseVisualStyleBackColor = true;
            // 
            // frmLineSetting
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 128);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblLineStatus);
            this.Controls.Add(this.txtLineName);
            this.Controls.Add(this.lblLineName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLineSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmLineSetting_Load);
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLineName;
        private System.Windows.Forms.TextBox txtLineName;
        private System.Windows.Forms.Label lblLineStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.RadioButton rbtnDeactive;
        private System.Windows.Forms.RadioButton rbtnActive;
    }
}