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
using MotorProtection.Core.Cache;
using MotorProtection.UI.Constant;
using MotorProtection.Core.Data.Entities;

namespace MotorProtection.UI
{
    public partial class frmMain : Form
    {
        private ServiceController _serviceCtrl = null;

        private byte[] RESET_REGISTER_ADDRESS = new byte[] { RegisterAddresses.ProtectorSettingHi, RegisterAddresses.ProtectorStatusResetLo };
        private byte[] CLEAR_ALARM = new byte[] { 0xa5, 0xa5 };
        private byte[] RESET = new byte[] { 0xaa, 0x55 };

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
                    tsslSystemStatus.Text = "已启动";
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

            ReloadDeviceTree();
        }

        private void tvProtectors_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point clickPoint = new Point(e.X, e.Y);
                TreeNode mySelect = tvProtectors.GetNodeAt(clickPoint);
                if (mySelect != null)
                    tvProtectors.SelectedNode = mySelect;
            }
        }

        private void tsmiAlarmSetting_Click(object sender, EventArgs e)
        {
            frmSystemSetting frmSystemSetting = new frmSystemSetting();
            frmSystemSetting.ShowDialog();
            if (frmSystemSetting.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                frmSystemSetting.Close();
            }
            else if (frmSystemSetting.DialogResult == System.Windows.Forms.DialogResult.OK || frmSystemSetting.DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                CacheController.UpdateAllCacheGroupTimestamp();
                frmSystemSetting.Close();
            }
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
            {
                lineSetting.Close();
            }
            else if (lineSetting.DialogResult == System.Windows.Forms.DialogResult.OK || lineSetting.DialogResult == System.Windows.Forms.DialogResult.Yes) // reload tree if operation is success
            {
                CacheController.UpdateAllCacheGroupTimestamp();
                ReloadDeviceTree();
                lineSetting.Close();
            }
        }

        private void tsmiAddProtectors_Click(object sender, EventArgs e)
        {
            frmProtectorSetting protectorSetting = new frmProtectorSetting(OperationKey.Add, null, null);
            protectorSetting.ShowDialog();
            if (protectorSetting.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                protectorSetting.Close();
            }
            else if (protectorSetting.DialogResult == System.Windows.Forms.DialogResult.OK || protectorSetting.DialogResult == System.Windows.Forms.DialogResult.Yes) // reload tree if operation is success
            {
                CacheController.UpdateAllCacheGroupTimestamp();
                ReloadDeviceTree();
                protectorSetting.Close();
            }
        }

        private void tsmiRightAddProtector_Click(object sender, EventArgs e)
        {
            frmProtectorSetting protectorSetting = new frmProtectorSetting(OperationKey.Add, null, tvProtectors.SelectedNode.Text);
            protectorSetting.ShowDialog();
            if (protectorSetting.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                protectorSetting.Close();
            }
            else if (protectorSetting.DialogResult == System.Windows.Forms.DialogResult.OK || protectorSetting.DialogResult == System.Windows.Forms.DialogResult.Yes) // reload tree if operation is success
            {
                CacheController.UpdateAllCacheGroupTimestamp();
                ReloadDeviceTree();
                protectorSetting.Close();
            }
        }

        private void tsmiRightEditProtector_Click(object sender, EventArgs e)
        {
            int deviceId = Convert.ToInt32(tvProtectors.SelectedNode.ToolTipText);
            Device device;

            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                device = ctt.Devices.Where(d => d.DeviceID == deviceId).FirstOrDefault();
                ctt.DeviceConfigs.Where(dc => dc.DeviceID == deviceId).ToList();
            }

            frmProtectorSetting protectorSetting = new frmProtectorSetting(OperationKey.Edit, device, tvProtectors.SelectedNode.Parent.Text);
            protectorSetting.ShowDialog();
            if (protectorSetting.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                protectorSetting.Close();
            }
            else if (protectorSetting.DialogResult == System.Windows.Forms.DialogResult.OK || protectorSetting.DialogResult == System.Windows.Forms.DialogResult.Yes) // reload tree if operation is success
            {
                CacheController.UpdateAllCacheGroupTimestamp();
                ReloadDeviceTree();
                protectorSetting.Close();
            }
        }

        private void tsmiRightDeactiveProtector_Click(object sender, EventArgs e)
        {
            int deviceId = Convert.ToInt32(tvProtectors.SelectedNode.ToolTipText);

            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var device = ctt.Devices.Where(d => d.DeviceID == deviceId).FirstOrDefault();
                if (device != null)
                {
                    device.IsActive = false;
                    ctt.SaveChanges();

                    CacheController.UpdateAllCacheGroupTimestamp();
                }
            }

            ReloadDeviceTree();
        }

        private void tsmiRightDeactive_Click(object sender, EventArgs e)
        {
            int deviceId = Convert.ToInt32(tvProtectors.SelectedNode.ToolTipText);

            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var parent = ctt.Devices.Where(d => d.DeviceID == deviceId).FirstOrDefault();
                if (parent != null)
                {
                    parent.IsActive = false;

                    var children = ctt.Devices.Where(d => d.ParentID == deviceId).ToList();
                    if (children != null && children.Count > 0)
                    {
                        foreach (Device child in children)
                        {
                            child.IsActive = false;
                        }
                    }
                    ctt.SaveChanges();

                    CacheController.UpdateAllCacheGroupTimestamp();
                }
            }

            ReloadDeviceTree();
        }

        private void tsmiRightLineEdit_Click(object sender, EventArgs e)
        {
            int deviceId = Convert.ToInt32(tvProtectors.SelectedNode.ToolTipText);
            Device device;

            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                device = ctt.Devices.Where(d => d.DeviceID == deviceId).FirstOrDefault();
            }

            frmLineSetting lineSetting = new frmLineSetting(OperationKey.Edit, device);
            lineSetting.ShowDialog();

            if (lineSetting.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                lineSetting.Close();
            }
            else if (lineSetting.DialogResult == System.Windows.Forms.DialogResult.OK || lineSetting.DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                CacheController.UpdateAllCacheGroupTimestamp();
                ReloadDeviceTree();
                lineSetting.Close();
            }
        }

        private void tsmiClearProtectorAlarm_Click(object sender, EventArgs e)
        {
            int deviceId = Convert.ToInt32(tvProtectors.SelectedNode.ToolTipText);
            DeviceConfigurationPool pool = new DeviceConfigurationPool();
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var device = ctt.Devices.Where(d => d.DeviceID == deviceId).FirstOrDefault();
                pool.Address = device.Address.Value;
                pool.FunCode = FunctionCodes.WRITE_SINGLE_REGISTER;
                pool.Commands = ParsingClearProtectorAlarmCommands();
                pool.Description = "";
                pool.UserID = 1;
                pool.CreateTime = DateTime.Now;
                pool.Attempt = 0;
                pool.Status = ConfigurationStatus.PROCESSING;
                pool.JobRemovable = false;

                ctt.DeviceConfigurationPools.AddObject(pool);
                ctt.SaveChanges();
            }

            frmMessage message = new frmMessage("正在清除保护器" + tvProtectors.SelectedNode.Text + "的报警，请稍后...", pool);
            message.ShowDialog();
            if (message.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                message.Close();
            }
            else if (message.DialogResult == System.Windows.Forms.DialogResult.None)
            {
                MessageBox.Show("清除报警失败，请重试或联系管理员");
                LogController.LogError(LoggingLevel.Error).Add("Description", "Clear protector " + tvProtectors.SelectedNode.Text + " alarm by user id: 1 at " + DateTime.Now.ToString()).Write();
            }
        }

        private void tsmiProtectorReset_Click(object sender, EventArgs e)
        {
            string name = tvProtectors.SelectedNode.Text;
            int deviceId = Convert.ToInt32(tvProtectors.SelectedNode.ToolTipText);
            DeviceConfigurationPool pool = new DeviceConfigurationPool();
            DialogResult result = MessageBox.Show("确认要复位保护器 "+name+" 吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                {
                    var device = ctt.Devices.Where(d => d.DeviceID == deviceId).FirstOrDefault();
                    pool.Address = device.Address.Value;
                    pool.FunCode = FunctionCodes.WRITE_SINGLE_REGISTER;
                    pool.Commands = ParsingProtectorResetCommands();
                    pool.Description = "";
                    pool.UserID = 1;
                    pool.CreateTime = DateTime.Now;
                    pool.Attempt = 0;
                    pool.Status = ConfigurationStatus.PROCESSING;
                    pool.JobRemovable = false;

                    ctt.DeviceConfigurationPools.AddObject(pool);
                    ctt.SaveChanges();
                }

                frmMessage message = new frmMessage("正在复位保护器" + name + "，请稍后...", pool);
                message.ShowDialog();
                if (message.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    message.Close();
                }
                else if (message.DialogResult == System.Windows.Forms.DialogResult.None)
                {
                    MessageBox.Show("复位失败，请重试或联系管理员");
                    LogController.LogError(LoggingLevel.Error).Add("Description", "Reset protector " + name + " by user id: 1 at " + DateTime.Now.ToString()).Write();
                }
            }
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

            tvProtectors.Nodes.Clear();

            if (parents.Count > 0)
            {
                foreach (Device parent in parents)
                {
                    TreeNode pNode = new TreeNode();
                    pNode.Text = parent.Name;
                    pNode.ToolTipText = parent.DeviceID.ToString();
                    pNode.ImageIndex = 3;
                    pNode.SelectedImageIndex = 3;
                    pNode.ContextMenuStrip = cmsParent;
                    tvProtectors.Nodes.Add(pNode);

                    var children = devices.Where(d => d.ParentID == parent.DeviceID && d.IsActive).ToList();
                    if (children.Count > 0)
                    {
                        foreach (Device child in children)
                        {
                            TreeNode cNode = new TreeNode();
                            cNode.Text = child.Name;
                            cNode.ToolTipText = child.DeviceID.ToString();
                            cNode.ContextMenuStrip = cmsChild;
                            if (child.Status == null || child.Status.Value == ProtectorStatus.Normal)
                            {
                                cNode.ImageIndex = 0;
                                cNode.SelectedImageIndex = 0;
                                cmsChild.Items["tsmiClearProtectorAlarm"].Enabled = false;
                            }
                            else if (child.Status.Value == ProtectorStatus.Stopped)
                            {
                                cNode.ImageIndex = 2;
                                cNode.SelectedImageIndex = 2;
                                cmsChild.Items["tsmiClearProtectorAlarm"].Enabled = false;
                            }
                            else if (child.Status.Value == ProtectorStatus.Alarm)
                            {
                                cNode.ImageIndex = 1;
                                cNode.SelectedImageIndex = 1;
                                cmsChild.Items["tsmiClearProtectorAlarm"].Enabled = true;
                            }
                            pNode.Nodes.Add(cNode);
                        }
                    }
                }
                tvProtectors.ExpandAll();
            }
        }

        private string ParsingClearProtectorAlarmCommands()
        {
            string command = "";

            StringBuilder builder = new StringBuilder();
            builder.Clear();

            foreach (byte addr in RESET_REGISTER_ADDRESS)
            {
                if (builder.Length != 0)
                    builder.Append(" ");
                builder.Append(addr.ToString("X2"));
            }

            foreach (byte o in CLEAR_ALARM)
            {
                if (builder.Length != 0)
                    builder.Append(" ");
                builder.Append(o.ToString("X2"));
            }

            command = builder.ToString();

            return command;
        }

        private string ParsingProtectorResetCommands()
        {
            string command = "";

            StringBuilder builder = new StringBuilder();
            builder.Clear();

            foreach (byte addr in RESET_REGISTER_ADDRESS)
            {
                if (builder.Length != 0)
                    builder.Append(" ");
                builder.Append(addr.ToString("X2"));
            }

            foreach (byte o in RESET)
            {
                if (builder.Length != 0)
                    builder.Append(" ");
                builder.Append(o.ToString("X2"));
            }

            command = builder.ToString();

            return command;
        }

        #endregion
    }
}
