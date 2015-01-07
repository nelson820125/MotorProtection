namespace MotorProtection.UI
{
    partial class frmMain
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
            this.menuSystem = new System.Windows.Forms.MenuStrip();
            this.tmsiSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAboutSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuSystem
            // 
            this.menuSystem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiSystem,
            this.tsmiAbout});
            this.menuSystem.Location = new System.Drawing.Point(0, 0);
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(1370, 24);
            this.menuSystem.TabIndex = 0;
            this.menuSystem.Text = "menuStrip1";
            // 
            // tmsiSystem
            // 
            this.tmsiSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiSignOut});
            this.tmsiSystem.Name = "tmsiSystem";
            this.tmsiSystem.Size = new System.Drawing.Size(45, 20);
            this.tmsiSystem.Text = "系统";
            // 
            // tmsiSignOut
            // 
            this.tmsiSignOut.Name = "tmsiSignOut";
            this.tmsiSignOut.Size = new System.Drawing.Size(152, 22);
            this.tmsiSignOut.Text = "退出";
            this.tmsiSignOut.Click += new System.EventHandler(this.tmsiSignOut_Click);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAboutSystem});
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(45, 20);
            this.tsmiAbout.Text = "关于";
            // 
            // tsmiAboutSystem
            // 
            this.tsmiAboutSystem.Name = "tsmiAboutSystem";
            this.tsmiAboutSystem.Size = new System.Drawing.Size(222, 22);
            this.tsmiAboutSystem.Text = "电机保护器控制软件V1.0...";
            this.tsmiAboutSystem.Click += new System.EventHandler(this.tsmiAboutSystem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 650);
            this.Controls.Add(this.menuSystem);
            this.MainMenuStrip = this.menuSystem;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Motor Protector Monitor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuSystem.ResumeLayout(false);
            this.menuSystem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuSystem;
        private System.Windows.Forms.ToolStripMenuItem tmsiSystem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiAboutSystem;
        private System.Windows.Forms.ToolStripMenuItem tmsiSignOut;
    }
}

