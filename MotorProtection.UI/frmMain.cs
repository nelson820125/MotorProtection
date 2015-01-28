﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using MotorProtection.Constant;
using MotorProtection.Core.Log;
using MotorProtection.Core.Cache;
using MotorProtection.UI.Constant;
using MotorProtection.Core.Data.Entities;

namespace MotorProtection.UI
{
    public partial class frmMain : Form
    {
        private ServiceController _serviceCtrl = null;

        public frmMain()
        {
            InitializeComponent();
            _serviceCtrl = Controller.WinServiceController.GetServiceByName(JobManagerKey.JOB_NAME);
            InitializeComponentStatus();
        }

        private void tsmiAboutSystem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("电机保护器控制软件V1.0\n技术支持：李宁");
        }

        private void tmsiSignOut_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        /// <summary>
        /// hide window border
        /// </summary>
        private void menuFullScreen_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new Point(0, 0);
            this.WindowState = FormWindowState.Maximized;
            menuExitFullScreen.Enabled = true;
            menuFullScreen.Enabled = false;
        }

        /// <summary>
        /// Show window border and min max and close buttons
        /// </summary>
        private void menuExitFullScreen_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new Point(0, 0);
            this.WindowState = FormWindowState.Maximized;
            menuExitFullScreen.Enabled = false;
            menuFullScreen.Enabled = true;
        }

        private void tsmiBasicParameterSetting_Click(object sender, EventArgs e)
        {
            frmBasicSetting frmSetting = new frmBasicSetting();
            frmSetting.ShowDialog();
            if (frmSetting.DialogResult == System.Windows.Forms.DialogResult.Cancel || frmSetting.DialogResult == System.Windows.Forms.DialogResult.OK || frmSetting.DialogResult == System.Windows.Forms.DialogResult.Yes)
                frmSetting.Close();
        }

        private void InitializeComponentStatus()
        {
            if (_serviceCtrl == null)
            {
                tsmiStart.Enabled = false;
                tsmiStop.Enabled = false;
                tsslSystemStatus.Text = "已停止";
                tsslSystemStatus.Image = Image.FromFile(@"images\icon\16x16\disconnect.png");
            }
            else
            {
                if (_serviceCtrl.Status == ServiceControllerStatus.Paused || _serviceCtrl.Status == ServiceControllerStatus.Stopped)
                {
                    tsmiStart.Enabled = true;
                    tsmiStop.Enabled = false;
                    tsslSystemStatus.Text = "已停止";
                    tsslSystemStatus.Image = Image.FromFile(@"images\icon\16x16\disconnect.png");
                }
                else
                {
                    tsmiStart.Enabled = false;
                    tsmiStop.Enabled = true;
                    tsslSystemStatus.Text = "已运行";
                    tsslSystemStatus.Image = Image.FromFile(@"images\icon\16x16\connect.png");
                }
            }
        }

        private void tsmiStart_Click(object sender, EventArgs e)
        {
            if (_serviceCtrl != null && (_serviceCtrl.Status == ServiceControllerStatus.Stopped || _serviceCtrl.Status == ServiceControllerStatus.Paused))
            {
                _serviceCtrl.Start();
                frmMessage frmMsg = new frmMessage("系统服务正在启动，请稍后...", _serviceCtrl, JobOperation.Start);
                frmMsg.ShowDialog();
                if (frmMsg.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    tsmiStart.Enabled = false;
                    tsmiStop.Enabled = true;
                    frmMsg.Close();
                }
                else if (frmMsg.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    frmMsg.Close();
                    LogController.LogError(LoggingLevel.Error).Add("Description", "Motor Protection Manager start failed.").Write();

                    frmMessage frmMsg1 = new frmMessage("操作失败，请重试或联系管理员");
                    frmMsg1.ShowDialog();
                    if (frmMsg1.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                        frmMsg1.Close();
                }
                else
                {
                    frmMsg.Close();
                }
            }                
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessage frmMsg = new frmMessage("操作失败，请重试或联系管理员");
            frmMsg.ShowDialog();
            DialogResult result = frmMsg.DialogResult;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            CacheController.Initialize();
        }

        private void tsmiAlarmSetting_Click(object sender, EventArgs e)
        {
            frmSystemSetting frmSystemSetting = new frmSystemSetting();
            frmSystemSetting.ShowDialog();
            if (frmSystemSetting.DialogResult == System.Windows.Forms.DialogResult.Cancel || frmSystemSetting.DialogResult == System.Windows.Forms.DialogResult.OK || frmSystemSetting.DialogResult == System.Windows.Forms.DialogResult.Yes)
                frmSystemSetting.Close();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void tsmiAddLines_Click(object sender, EventArgs e)
        {
            frmLineSetting lineSetting = new frmLineSetting(OperationKey.Add, null);
            lineSetting.ShowDialog();
            if (lineSetting.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                lineSetting.Close();
            else if (lineSetting.DialogResult == System.Windows.Forms.DialogResult.OK || lineSetting.DialogResult == System.Windows.Forms.DialogResult.Yes) // reload tree if operation is success
                ReloadDeviceTree();
        }

        #region private

        private void ReloadDeviceTree()
        {
            List<Device> devices = null;

            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                devices = ctt.Devices.Where(d => d.IsActive).ToList();
            }

            var parents = devices.Where(d => d.ParentID == null && d.IsActive).ToList();

            foreach (Device parent in parents)
            {
                TreeNode pNode = new TreeNode();
                pNode.Text = parent.Name;
                tvProtectors.Nodes.Add(pNode);

                var children = devices.Where(d => d.ParentID == parent.DeviceID && d.IsActive).ToList();
                if (children.Count > 0)
                {
                    foreach (Device child in children)
                    {
                        TreeNode cNode = new TreeNode();
                        cNode.Name = child.Name;
                        if (child.Status == null || child.Status.Value == ProtectorStatus.Normal)
                            cNode.ImageIndex = 0;
                        else if (child.Status.Value == ProtectorStatus.Stopped)
                            cNode.ImageIndex = 2;
                        else if (child.Status.Value == ProtectorStatus.Alarm)
                            cNode.ImageIndex = 1;
                        pNode.Nodes.Add(cNode);
                    }
                }
            }
        }

        #endregion
    }
}
