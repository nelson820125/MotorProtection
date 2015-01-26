using System;
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
        }

        private void InitializeComponentStatus()
        {
            if (_serviceCtrl == null)
            {
                tsmiStart.Enabled = false;
                tsmiStop.Enabled = false;
            }
            else
            {
                if (_serviceCtrl.Status == ServiceControllerStatus.Paused || _serviceCtrl.Status == ServiceControllerStatus.Stopped)
                {
                    tsmiStart.Enabled = true;
                    tsmiStop.Enabled = false;
                }
                else
                {
                    tsmiStart.Enabled = false;
                    tsmiStop.Enabled = true;
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
                    LogController.LogError(LoggingLevel.Level1).Add("Description", "Motor Protection Manager start failed.").Write();

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
    }
}
