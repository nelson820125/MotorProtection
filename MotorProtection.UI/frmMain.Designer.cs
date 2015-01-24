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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuSystem = new System.Windows.Forms.MenuStrip();
            this.tmsiSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAboutSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.stsBottom = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExitFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystem.SuspendLayout();
            this.stsBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuSystem
            // 
            this.menuSystem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiSystem,
            this.menuView,
            this.tsmiAbout});
            this.menuSystem.Location = new System.Drawing.Point(0, 0);
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(1370, 25);
            this.menuSystem.TabIndex = 0;
            this.menuSystem.Text = "menuStrip1";
            // 
            // tmsiSystem
            // 
            this.tmsiSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiSignOut});
            this.tmsiSystem.Name = "tmsiSystem";
            this.tmsiSystem.Size = new System.Drawing.Size(44, 21);
            this.tmsiSystem.Text = "系统";
            // 
            // tmsiSignOut
            // 
            this.tmsiSignOut.Name = "tmsiSignOut";
            this.tmsiSignOut.Size = new System.Drawing.Size(100, 22);
            this.tmsiSignOut.Text = "退出";
            this.tmsiSignOut.Click += new System.EventHandler(this.tmsiSignOut_Click);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAboutSystem});
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(44, 21);
            this.tsmiAbout.Text = "关于";
            // 
            // tsmiAboutSystem
            // 
            this.tsmiAboutSystem.Name = "tsmiAboutSystem";
            this.tsmiAboutSystem.Size = new System.Drawing.Size(218, 22);
            this.tsmiAboutSystem.Text = "电机保护器控制软件V1.0...";
            this.tsmiAboutSystem.Click += new System.EventHandler(this.tsmiAboutSystem_Click);
            // 
            // stsBottom
            // 
            this.stsBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.stsBottom.Location = new System.Drawing.Point(0, 578);
            this.stsBottom.Name = "stsBottom";
            this.stsBottom.Size = new System.Drawing.Size(1370, 22);
            this.stsBottom.TabIndex = 1;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(233, 17);
            this.toolStripStatusLabel1.Text = "Copyright 2015 大连环宇佳机电有限公司";
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFullScreen,
            this.menuExitFullScreen});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(44, 21);
            this.menuView.Text = "视图";
            // 
            // menuFullScreen
            // 
            this.menuFullScreen.Name = "menuFullScreen";
            this.menuFullScreen.Size = new System.Drawing.Size(152, 22);
            this.menuFullScreen.Text = "全屏";
            this.menuFullScreen.Click += new System.EventHandler(this.menuFullScreen_Click);
            // 
            // menuExitFullScreen
            // 
            this.menuExitFullScreen.Enabled = false;
            this.menuExitFullScreen.Name = "menuExitFullScreen";
            this.menuExitFullScreen.Size = new System.Drawing.Size(152, 22);
            this.menuExitFullScreen.Text = "退出全屏";
            this.menuExitFullScreen.Click += new System.EventHandler(this.menuExitFullScreen_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 600);
            this.Controls.Add(this.stsBottom);
            this.Controls.Add(this.menuSystem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuSystem;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保护器监控系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuSystem.ResumeLayout(false);
            this.menuSystem.PerformLayout();
            this.stsBottom.ResumeLayout(false);
            this.stsBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuSystem;
        private System.Windows.Forms.ToolStripMenuItem tmsiSystem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiAboutSystem;
        private System.Windows.Forms.ToolStripMenuItem tmsiSignOut;
        private System.Windows.Forms.StatusStrip stsBottom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuFullScreen;
        private System.Windows.Forms.ToolStripMenuItem menuExitFullScreen;
    }
}

