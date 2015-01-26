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
            this.tsmiStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsiSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBasicParameterSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExitFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAboutSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stsBottom = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuSystem.SuspendLayout();
            this.stsBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuSystem
            // 
            this.menuSystem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiSystem,
            this.tsmiEdit,
            this.menuView,
            this.tsmiAbout,
            this.testToolStripMenuItem});
            this.menuSystem.Location = new System.Drawing.Point(0, 0);
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(1370, 24);
            this.menuSystem.TabIndex = 0;
            this.menuSystem.Text = "menuStrip1";
            // 
            // tmsiSystem
            // 
            this.tmsiSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStart,
            this.tsmiStop,
            this.toolStripSeparator1,
            this.tmsiSignOut});
            this.tmsiSystem.Name = "tmsiSystem";
            this.tmsiSystem.Size = new System.Drawing.Size(45, 20);
            this.tmsiSystem.Text = "系统";
            // 
            // tsmiStart
            // 
            this.tsmiStart.Image = ((System.Drawing.Image)(resources.GetObject("tsmiStart.Image")));
            this.tsmiStart.Name = "tsmiStart";
            this.tsmiStart.Size = new System.Drawing.Size(126, 22);
            this.tsmiStart.Text = "启动系统";
            this.tsmiStart.Click += new System.EventHandler(this.tsmiStart_Click);
            // 
            // tsmiStop
            // 
            this.tsmiStop.Image = ((System.Drawing.Image)(resources.GetObject("tsmiStop.Image")));
            this.tsmiStop.Name = "tsmiStop";
            this.tsmiStop.Size = new System.Drawing.Size(126, 22);
            this.tsmiStop.Text = "停止系统";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // tmsiSignOut
            // 
            this.tmsiSignOut.Name = "tmsiSignOut";
            this.tmsiSignOut.Size = new System.Drawing.Size(126, 22);
            this.tmsiSignOut.Text = "退出";
            this.tmsiSignOut.Click += new System.EventHandler(this.tmsiSignOut_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBasicParameterSetting});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(45, 20);
            this.tsmiEdit.Text = "设置";
            // 
            // tsmiBasicParameterSetting
            // 
            this.tsmiBasicParameterSetting.Name = "tsmiBasicParameterSetting";
            this.tsmiBasicParameterSetting.Size = new System.Drawing.Size(126, 22);
            this.tsmiBasicParameterSetting.Text = "基本参数";
            this.tsmiBasicParameterSetting.Click += new System.EventHandler(this.tsmiBasicParameterSetting_Click);
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFullScreen,
            this.menuExitFullScreen});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(45, 20);
            this.menuView.Text = "视图";
            // 
            // menuFullScreen
            // 
            this.menuFullScreen.Name = "menuFullScreen";
            this.menuFullScreen.Size = new System.Drawing.Size(126, 22);
            this.menuFullScreen.Text = "全屏";
            this.menuFullScreen.Click += new System.EventHandler(this.menuFullScreen_Click);
            // 
            // menuExitFullScreen
            // 
            this.menuExitFullScreen.Enabled = false;
            this.menuExitFullScreen.Name = "menuExitFullScreen";
            this.menuExitFullScreen.Size = new System.Drawing.Size(126, 22);
            this.menuExitFullScreen.Text = "退出全屏";
            this.menuExitFullScreen.Click += new System.EventHandler(this.menuExitFullScreen_Click);
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
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.testToolStripMenuItem.Text = "test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // stsBottom
            // 
            this.stsBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.stsBottom.Location = new System.Drawing.Point(0, 628);
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
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 650);
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
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiBasicParameterSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiStart;
        private System.Windows.Forms.ToolStripMenuItem tsmiStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    }
}

