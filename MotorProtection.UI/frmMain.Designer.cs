﻿namespace MotorProtection.UI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuSystem = new System.Windows.Forms.MenuStrip();
            this.tmsiSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsiSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBasicParameterSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAlarmSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddNodes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddLines = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddProtectors = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExitFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAboutSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.stsBottom = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSystemStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlNodes = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tvProtectors = new System.Windows.Forms.TreeView();
            this.imgListStatus = new System.Windows.Forms.ImageList(this.components);
            this.pnlShowOrHideNode = new System.Windows.Forms.Panel();
            this.btnFixedOrHide = new System.Windows.Forms.Button();
            this.lblNodesTitle = new System.Windows.Forms.Label();
            this.pnlMainShow = new System.Windows.Forms.Panel();
            this.cmsParent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRightAddProtector = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRightLineEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRightDeactive = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsChild = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRightEditProtector = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRightDeactiveProtector = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProtectParameterSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClearProtectorAlarm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProtectorReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHide = new System.Windows.Forms.ToolStripMenuItem();
            this.timFresh = new System.Windows.Forms.Timer(this.components);
            this.pnlShortNavigator = new System.Windows.Forms.Panel();
            this.lblShortTreeNavigator = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlMainContainer = new System.Windows.Forms.Panel();
            this.menuSystem.SuspendLayout();
            this.stsBottom.SuspendLayout();
            this.pnlNodes.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlShowOrHideNode.SuspendLayout();
            this.cmsParent.SuspendLayout();
            this.cmsChild.SuspendLayout();
            this.pnlShortNavigator.SuspendLayout();
            this.pnlMainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuSystem
            // 
            this.menuSystem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiSystem,
            this.tsmiEdit,
            this.menuView,
            this.tsmiAbout});
            this.menuSystem.Location = new System.Drawing.Point(0, 0);
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuSystem.Size = new System.Drawing.Size(1827, 28);
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
            this.tmsiSystem.Size = new System.Drawing.Size(53, 24);
            this.tmsiSystem.Text = "系统";
            // 
            // tsmiStart
            // 
            this.tsmiStart.Image = ((System.Drawing.Image)(resources.GetObject("tsmiStart.Image")));
            this.tsmiStart.Name = "tsmiStart";
            this.tsmiStart.Size = new System.Drawing.Size(175, 24);
            this.tsmiStart.Text = "启动系统";
            this.tsmiStart.Click += new System.EventHandler(this.tsmiStart_Click);
            // 
            // tsmiStop
            // 
            this.tsmiStop.Image = ((System.Drawing.Image)(resources.GetObject("tsmiStop.Image")));
            this.tsmiStop.Name = "tsmiStop";
            this.tsmiStop.Size = new System.Drawing.Size(142, 24);
            this.tsmiStop.Text = "停止系统";
            this.tsmiStop.Click += new System.EventHandler(this.tsmiStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // tmsiSignOut
            // 
            this.tmsiSignOut.Name = "tmsiSignOut";
            this.tmsiSignOut.Size = new System.Drawing.Size(142, 24);
            this.tmsiSignOut.Text = "退出";
            this.tmsiSignOut.Click += new System.EventHandler(this.tmsiSignOut_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBasicParameterSetting,
            this.tsmiAlarmSetting,
            this.toolStripSeparator2,
            this.tsmiAddNodes});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(53, 24);
            this.tsmiEdit.Text = "设置";
            // 
            // tsmiBasicParameterSetting
            // 
            this.tsmiBasicParameterSetting.Name = "tsmiBasicParameterSetting";
            this.tsmiBasicParameterSetting.Size = new System.Drawing.Size(142, 24);
            this.tsmiBasicParameterSetting.Text = "通讯设置";
            this.tsmiBasicParameterSetting.Click += new System.EventHandler(this.tsmiBasicParameterSetting_Click);
            // 
            // tsmiAlarmSetting
            // 
            this.tsmiAlarmSetting.Name = "tsmiAlarmSetting";
            this.tsmiAlarmSetting.Size = new System.Drawing.Size(142, 24);
            this.tsmiAlarmSetting.Text = "系统设置";
            this.tsmiAlarmSetting.Click += new System.EventHandler(this.tsmiAlarmSetting_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(139, 6);
            // 
            // tsmiAddNodes
            // 
            this.tsmiAddNodes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddLines,
            this.tsmiAddProtectors});
            this.tsmiAddNodes.Name = "tsmiAddNodes";
            this.tsmiAddNodes.Size = new System.Drawing.Size(142, 24);
            this.tsmiAddNodes.Text = "添加节点";
            // 
            // tsmiAddLines
            // 
            this.tsmiAddLines.Name = "tsmiAddLines";
            this.tsmiAddLines.Size = new System.Drawing.Size(158, 24);
            this.tsmiAddLines.Text = "添加生产线";
            this.tsmiAddLines.Click += new System.EventHandler(this.tsmiAddLines_Click);
            // 
            // tsmiAddProtectors
            // 
            this.tsmiAddProtectors.Name = "tsmiAddProtectors";
            this.tsmiAddProtectors.Size = new System.Drawing.Size(158, 24);
            this.tsmiAddProtectors.Text = "添加保护器";
            this.tsmiAddProtectors.Click += new System.EventHandler(this.tsmiAddProtectors_Click);
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFullScreen,
            this.menuExitFullScreen});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(53, 24);
            this.menuView.Text = "视图";
            // 
            // menuFullScreen
            // 
            this.menuFullScreen.Name = "menuFullScreen";
            this.menuFullScreen.Size = new System.Drawing.Size(142, 24);
            this.menuFullScreen.Text = "全屏";
            this.menuFullScreen.Click += new System.EventHandler(this.menuFullScreen_Click);
            // 
            // menuExitFullScreen
            // 
            this.menuExitFullScreen.Enabled = false;
            this.menuExitFullScreen.Name = "menuExitFullScreen";
            this.menuExitFullScreen.Size = new System.Drawing.Size(142, 24);
            this.menuExitFullScreen.Text = "退出全屏";
            this.menuExitFullScreen.Click += new System.EventHandler(this.menuExitFullScreen_Click);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAboutSystem});
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(53, 24);
            this.tsmiAbout.Text = "关于";
            // 
            // tsmiAboutSystem
            // 
            this.tsmiAboutSystem.Name = "tsmiAboutSystem";
            this.tsmiAboutSystem.Size = new System.Drawing.Size(259, 24);
            this.tsmiAboutSystem.Text = "电机保护器控制软件V1.0...";
            this.tsmiAboutSystem.Click += new System.EventHandler(this.tsmiAboutSystem_Click);
            // 
            // stsBottom
            // 
            this.stsBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsslSystemStatus});
            this.stsBottom.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.stsBottom.Location = new System.Drawing.Point(0, 775);
            this.stsBottom.Name = "stsBottom";
            this.stsBottom.Padding = new System.Windows.Forms.Padding(19, 0, 1, 0);
            this.stsBottom.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.stsBottom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stsBottom.Size = new System.Drawing.Size(1827, 25);
            this.stsBottom.TabIndex = 1;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(290, 20);
            this.toolStripStatusLabel1.Text = "Copyright 2015 大连环宇佳机电有限公司";
            // 
            // tsslSystemStatus
            // 
            this.tsslSystemStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tsslSystemStatus.Name = "tsslSystemStatus";
            this.tsslSystemStatus.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tsslSystemStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tsslSystemStatus.Size = new System.Drawing.Size(20, 20);
            // 
            // pnlNodes
            // 
            this.pnlNodes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlNodes.Controls.Add(this.panel1);
            this.pnlNodes.Controls.Add(this.pnlShowOrHideNode);
            this.pnlNodes.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNodes.Location = new System.Drawing.Point(0, 28);
            this.pnlNodes.Margin = new System.Windows.Forms.Padding(4);
            this.pnlNodes.Name = "pnlNodes";
            this.pnlNodes.Size = new System.Drawing.Size(265, 747);
            this.pnlNodes.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tvProtectors);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 717);
            this.panel1.TabIndex = 2;
            // 
            // tvProtectors
            // 
            this.tvProtectors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvProtectors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProtectors.ImageIndex = 3;
            this.tvProtectors.ImageList = this.imgListStatus;
            this.tvProtectors.Indent = 19;
            this.tvProtectors.ItemHeight = 20;
            this.tvProtectors.LineColor = System.Drawing.Color.White;
            this.tvProtectors.Location = new System.Drawing.Point(0, 0);
            this.tvProtectors.Margin = new System.Windows.Forms.Padding(4);
            this.tvProtectors.Name = "tvProtectors";
            this.tvProtectors.SelectedImageIndex = 0;
            this.tvProtectors.Size = new System.Drawing.Size(261, 717);
            this.tvProtectors.TabIndex = 1;
            this.tvProtectors.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProtectors_MouseDown);
            // 
            // imgListStatus
            // 
            this.imgListStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListStatus.ImageStream")));
            this.imgListStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListStatus.Images.SetKeyName(0, "running.png");
            this.imgListStatus.Images.SetKeyName(1, "alarm.png");
            this.imgListStatus.Images.SetKeyName(2, "stop.png");
            this.imgListStatus.Images.SetKeyName(3, "server.png");
            // 
            // pnlShowOrHideNode
            // 
            this.pnlShowOrHideNode.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.pnlShowOrHideNode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlShowOrHideNode.Controls.Add(this.btnFixedOrHide);
            this.pnlShowOrHideNode.Controls.Add(this.lblNodesTitle);
            this.pnlShowOrHideNode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlShowOrHideNode.Location = new System.Drawing.Point(0, 0);
            this.pnlShowOrHideNode.Name = "pnlShowOrHideNode";
            this.pnlShowOrHideNode.Size = new System.Drawing.Size(261, 26);
            this.pnlShowOrHideNode.TabIndex = 0;
            // 
            // btnFixedOrHide
            // 
            this.btnFixedOrHide.AutoSize = true;
            this.btnFixedOrHide.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnFixedOrHide.BackgroundImage = global::MotorProtection.UI.Properties.Resources._fixed;
            this.btnFixedOrHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFixedOrHide.FlatAppearance.BorderSize = 0;
            this.btnFixedOrHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFixedOrHide.Location = new System.Drawing.Point(235, 2);
            this.btnFixedOrHide.Name = "btnFixedOrHide";
            this.btnFixedOrHide.Size = new System.Drawing.Size(20, 20);
            this.btnFixedOrHide.TabIndex = 1;
            this.btnFixedOrHide.UseVisualStyleBackColor = false;
            this.btnFixedOrHide.Click += new System.EventHandler(this.btnFixedOrHide_Click);
            // 
            // lblNodesTitle
            // 
            this.lblNodesTitle.AutoSize = true;
            this.lblNodesTitle.ForeColor = System.Drawing.Color.White;
            this.lblNodesTitle.Location = new System.Drawing.Point(3, 3);
            this.lblNodesTitle.Name = "lblNodesTitle";
            this.lblNodesTitle.Size = new System.Drawing.Size(78, 17);
            this.lblNodesTitle.TabIndex = 0;
            this.lblNodesTitle.Text = "树型导航栏";
            // 
            // pnlMainShow
            // 
            this.pnlMainShow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMainShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlMainShow.Location = new System.Drawing.Point(0, 0);
            this.pnlMainShow.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMainShow.Name = "pnlMainShow";
            this.pnlMainShow.Size = new System.Drawing.Size(1520, 747);
            this.pnlMainShow.TabIndex = 5;
            // 
            // cmsParent
            // 
            this.cmsParent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRightAddProtector,
            this.toolStripSeparator3,
            this.tsmiRightLineEdit,
            this.tsmiRightDeactive});
            this.cmsParent.Name = "cmsParent";
            this.cmsParent.Size = new System.Drawing.Size(159, 82);
            // 
            // tsmiRightAddProtector
            // 
            this.tsmiRightAddProtector.Name = "tsmiRightAddProtector";
            this.tsmiRightAddProtector.Size = new System.Drawing.Size(158, 24);
            this.tsmiRightAddProtector.Text = "添加保护器";
            this.tsmiRightAddProtector.Click += new System.EventHandler(this.tsmiRightAddProtector_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(155, 6);
            // 
            // tsmiRightLineEdit
            // 
            this.tsmiRightLineEdit.Name = "tsmiRightLineEdit";
            this.tsmiRightLineEdit.Size = new System.Drawing.Size(158, 24);
            this.tsmiRightLineEdit.Text = "编辑";
            this.tsmiRightLineEdit.Click += new System.EventHandler(this.tsmiRightLineEdit_Click);
            // 
            // tsmiRightDeactive
            // 
            this.tsmiRightDeactive.Name = "tsmiRightDeactive";
            this.tsmiRightDeactive.Size = new System.Drawing.Size(158, 24);
            this.tsmiRightDeactive.Text = "禁用";
            this.tsmiRightDeactive.Click += new System.EventHandler(this.tsmiRightDeactive_Click);
            // 
            // cmsChild
            // 
            this.cmsChild.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRightEditProtector,
            this.tsmiRightDeactiveProtector,
            this.tsmiProtectParameterSetting,
            this.toolStripSeparator4,
            this.tsmiClearProtectorAlarm,
            this.tsmiProtectorReset,
            this.toolStripSeparator5,
            this.tsmiDisplay,
            this.tsmiHide});
            this.cmsChild.Name = "cmsChild";
            this.cmsChild.Size = new System.Drawing.Size(191, 184);
            // 
            // tsmiRightEditProtector
            // 
            this.tsmiRightEditProtector.Name = "tsmiRightEditProtector";
            this.tsmiRightEditProtector.Size = new System.Drawing.Size(190, 24);
            this.tsmiRightEditProtector.Text = "编辑";
            this.tsmiRightEditProtector.Click += new System.EventHandler(this.tsmiRightEditProtector_Click);
            // 
            // tsmiRightDeactiveProtector
            // 
            this.tsmiRightDeactiveProtector.Name = "tsmiRightDeactiveProtector";
            this.tsmiRightDeactiveProtector.Size = new System.Drawing.Size(190, 24);
            this.tsmiRightDeactiveProtector.Text = "禁用";
            this.tsmiRightDeactiveProtector.Click += new System.EventHandler(this.tsmiRightDeactiveProtector_Click);
            // 
            // tsmiProtectParameterSetting
            // 
            this.tsmiProtectParameterSetting.Name = "tsmiProtectParameterSetting";
            this.tsmiProtectParameterSetting.Size = new System.Drawing.Size(190, 24);
            this.tsmiProtectParameterSetting.Text = "保护参数设置";
            this.tsmiProtectParameterSetting.Click += new System.EventHandler(this.tsmiProtectParameterSetting_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(187, 6);
            // 
            // tsmiClearProtectorAlarm
            // 
            this.tsmiClearProtectorAlarm.Name = "tsmiClearProtectorAlarm";
            this.tsmiClearProtectorAlarm.Size = new System.Drawing.Size(190, 24);
            this.tsmiClearProtectorAlarm.Text = "解除警报";
            this.tsmiClearProtectorAlarm.Click += new System.EventHandler(this.tsmiClearProtectorAlarm_Click);
            // 
            // tsmiProtectorReset
            // 
            this.tsmiProtectorReset.Name = "tsmiProtectorReset";
            this.tsmiProtectorReset.Size = new System.Drawing.Size(190, 24);
            this.tsmiProtectorReset.Text = "复位";
            this.tsmiProtectorReset.Click += new System.EventHandler(this.tsmiProtectorReset_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(187, 6);
            // 
            // tsmiDisplay
            // 
            this.tsmiDisplay.Name = "tsmiDisplay";
            this.tsmiDisplay.Size = new System.Drawing.Size(190, 24);
            this.tsmiDisplay.Text = "在主显示区显示";
            this.tsmiDisplay.Click += new System.EventHandler(this.tsmiDisplay_Click);
            // 
            // tsmiHide
            // 
            this.tsmiHide.Name = "tsmiHide";
            this.tsmiHide.Size = new System.Drawing.Size(190, 24);
            this.tsmiHide.Text = "在主显示区隐藏";
            this.tsmiHide.Click += new System.EventHandler(this.tsmiHide_Click);
            // 
            // timFresh
            // 
            this.timFresh.Interval = 5000;
            // 
            // pnlShortNavigator
            // 
            this.pnlShortNavigator.Controls.Add(this.lblShortTreeNavigator);
            this.pnlShortNavigator.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlShortNavigator.Location = new System.Drawing.Point(265, 28);
            this.pnlShortNavigator.Name = "pnlShortNavigator";
            this.pnlShortNavigator.Size = new System.Drawing.Size(39, 747);
            this.pnlShortNavigator.TabIndex = 3;
            this.pnlShortNavigator.Visible = false;
            // 
            // lblShortTreeNavigator
            // 
            this.lblShortTreeNavigator.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblShortTreeNavigator.Location = new System.Drawing.Point(9, 8);
            this.lblShortTreeNavigator.Name = "lblShortTreeNavigator";
            this.lblShortTreeNavigator.Size = new System.Drawing.Size(22, 93);
            this.lblShortTreeNavigator.TabIndex = 0;
            this.lblShortTreeNavigator.Text = "树型导航栏";
            this.lblShortTreeNavigator.Click += new System.EventHandler(this.lblShortTreeNavigator_Click);
            this.lblShortTreeNavigator.MouseLeave += new System.EventHandler(this.lblShortTreeNavigator_MouseLeave);
            this.lblShortTreeNavigator.MouseHover += new System.EventHandler(this.lblShortTreeNavigator_MouseHover);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(304, 28);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 747);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // pnlMainContainer
            // 
            this.pnlMainContainer.Controls.Add(this.pnlMainShow);
            this.pnlMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContainer.Location = new System.Drawing.Point(307, 28);
            this.pnlMainContainer.Name = "pnlMainContainer";
            this.pnlMainContainer.Size = new System.Drawing.Size(1520, 747);
            this.pnlMainContainer.TabIndex = 5;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1827, 800);
            this.Controls.Add(this.pnlMainContainer);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlShortNavigator);
            this.Controls.Add(this.pnlNodes);
            this.Controls.Add(this.stsBottom);
            this.Controls.Add(this.menuSystem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuSystem;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保护器监控系统 v1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuSystem.ResumeLayout(false);
            this.menuSystem.PerformLayout();
            this.stsBottom.ResumeLayout(false);
            this.stsBottom.PerformLayout();
            this.pnlNodes.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlShowOrHideNode.ResumeLayout(false);
            this.pnlShowOrHideNode.PerformLayout();
            this.cmsParent.ResumeLayout(false);
            this.cmsChild.ResumeLayout(false);
            this.pnlShortNavigator.ResumeLayout(false);
            this.pnlMainContainer.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem tsmiAlarmSetting;
        private System.Windows.Forms.Panel pnlNodes;
        private System.Windows.Forms.Panel pnlMainShow;
        private System.Windows.Forms.ToolStripStatusLabel tsslSystemStatus;
        private System.Windows.Forms.TreeView tvProtectors;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddNodes;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddLines;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddProtectors;
        private System.Windows.Forms.ImageList imgListStatus;
        private System.Windows.Forms.ContextMenuStrip cmsParent;
        private System.Windows.Forms.ContextMenuStrip cmsChild;
        private System.Windows.Forms.ToolStripMenuItem tsmiRightAddProtector;
        private System.Windows.Forms.ToolStripMenuItem tsmiRightDeactive;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiRightLineEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiRightEditProtector;
        private System.Windows.Forms.ToolStripMenuItem tsmiRightDeactiveProtector;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearProtectorAlarm;
        private System.Windows.Forms.ToolStripMenuItem tsmiProtectorReset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisplay;
        private System.Windows.Forms.ToolStripMenuItem tsmiHide;
        private System.Windows.Forms.Timer timFresh;
        private System.Windows.Forms.Panel pnlShowOrHideNode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNodesTitle;
        private System.Windows.Forms.Button btnFixedOrHide;
        private System.Windows.Forms.ToolStripMenuItem tsmiProtectParameterSetting;
        private System.Windows.Forms.Panel pnlShortNavigator;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlMainContainer;
        private System.Windows.Forms.Label lblShortTreeNavigator;
    }
}

