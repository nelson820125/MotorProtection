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
                {
                    tvProtectors.SelectedNode = mySelect;
                    int deviceId = Convert.ToInt32(mySelect.ToolTipText);
                    using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                    {
                        var device = ctt.Devices.Where(d => d.DeviceID == deviceId).FirstOrDefault();

                        if (device != null)
                        {
                            if (device.ParentID != null && device.ParentID.Value != 0) // is sub tree node
                            {
                                if (device.Status == null || device.Status.Value == ProtectorStatus.Normal)
                                {
                                    cmsChild.Items["tsmiClearProtectorAlarm"].Enabled = false;
                                }
                                else if (device.Status.Value == ProtectorStatus.Stopped)
                                {
                                    cmsChild.Items["tsmiClearProtectorAlarm"].Enabled = false;
                                }
                                else if (device.Status.Value == ProtectorStatus.Alarm)
                                {
                                    cmsChild.Items["tsmiClearProtectorAlarm"].Enabled = true;
                                }

                                if (device.IsDisplay)
                                {
                                    cmsChild.Items["tsmiDisplay"].Enabled = false;
                                    cmsChild.Items["tsmiHide"].Enabled = true;
                                }
                                else
                                {
                                    cmsChild.Items["tsmiDisplay"].Enabled = true;
                                    cmsChild.Items["tsmiHide"].Enabled = false;
                                }
                            }
                        }
                    }
                }
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

        private void ClearProtectorAlarm(int deviceId)
        {
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
                CacheController.UpdateAllCacheGroupTimestamp();
                ReloadDeviceTree();
                message.Close();
            }
            else if (message.DialogResult == System.Windows.Forms.DialogResult.None)
            {
                MessageBox.Show("清除报警失败，请重试或联系管理员");
                LogController.LogError(LoggingLevel.Error).Add("Description", "Clear protector " + tvProtectors.SelectedNode.Text + " alarm by user id: 1 at " + DateTime.Now.ToString()).Write();
            }
        }

        private void tsmiClearProtectorAlarm_Click(object sender, EventArgs e)
        {
            int deviceId = Convert.ToInt32(tvProtectors.SelectedNode.ToolTipText);
            ClearProtectorAlarm(deviceId);
        }

        private void mainButtonClearAlarm_Click(object sender, EventArgs e)
        {
            Label lblDeviceID = (Label)((Button)sender).Parent.Controls["lblDeviceID"];
            ClearProtectorAlarm(Convert.ToInt32(lblDeviceID.Text.Trim()));
        }

        private void ResetProtector(string name, int deviceId)
        {
            DeviceConfigurationPool pool = new DeviceConfigurationPool();
            DialogResult result = MessageBox.Show("确认要复位保护器 " + name + " 吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                    CacheController.UpdateAllCacheGroupTimestamp();
                    ReloadDeviceTree();
                    message.Close();
                }
                else if (message.DialogResult == System.Windows.Forms.DialogResult.None)
                {
                    MessageBox.Show("复位失败，请重试或联系管理员");
                    LogController.LogError(LoggingLevel.Error).Add("Description", "Reset protector " + name + " by user id: 1 at " + DateTime.Now.ToString()).Write();
                }
            }
        }

        private void tsmiProtectorReset_Click(object sender, EventArgs e)
        {
            string name = tvProtectors.SelectedNode.Text;
            int deviceId = Convert.ToInt32(tvProtectors.SelectedNode.ToolTipText);
            ResetProtector(name, deviceId);
        }

        private void mainButtonReset_Click(object sender, EventArgs e)
        {
            Label lblProtectorNameValue = (Label)((Button)sender).Parent.Controls["lblProtectorNameValue"];
            Label lblDeviceID = (Label)((Button)sender).Parent.Controls["lblDeviceID"];
            ResetProtector(lblProtectorNameValue.Text.Trim(), Convert.ToInt32(lblDeviceID.Text.Trim()));
        }

        private void tsmiDisplay_Click(object sender, EventArgs e)
        {
            int deviceId = Convert.ToInt32(tvProtectors.SelectedNode.ToolTipText);

            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var device = ctt.Devices.Where(d => d.DeviceID == deviceId).FirstOrDefault();

                if (device != null)
                {
                    device.IsDisplay = true;
                    device.UpdateTime = DateTime.Now;
                }

                ctt.SaveChanges();
            }

            CacheController.UpdateAllCacheGroupTimestamp();
            ReloadDeviceTree();
            ReloadMainPanel();
        }

        private void HideProtectorPanel(int deviceId)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var device = ctt.Devices.Where(d => d.DeviceID == deviceId).FirstOrDefault();

                if (device != null)
                {
                    device.IsDisplay = false;
                    device.UpdateTime = DateTime.Now;
                }

                ctt.SaveChanges();
            }

            CacheController.UpdateAllCacheGroupTimestamp();
            ReloadDeviceTree();
            ReloadMainPanel();
        }

        private void tsmiHide_Click(object sender, EventArgs e)
        {
            int deviceId = Convert.ToInt32(tvProtectors.SelectedNode.ToolTipText);

            HideProtectorPanel(deviceId);
        }

        private void mainButtonHide_Click(object sender, EventArgs e)
        {
            Label lblDeviceID = (Label)((Button)sender).Parent.Controls["lblDeviceID"];
            int deviceId = Convert.ToInt32(lblDeviceID.Text.Trim());

            HideProtectorPanel(deviceId);
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

                            if (child.IsDisplay)
                            {
                                cmsChild.Items["tsmiDisplay"].Enabled = false;
                                cmsChild.Items["tsmiHide"].Enabled = true;
                            }
                            else
                            {
                                cmsChild.Items["tsmiDisplay"].Enabled = true;
                                cmsChild.Items["tsmiHide"].Enabled = false;
                            }

                            pNode.Nodes.Add(cNode);
                        }
                    }
                }
                tvProtectors.ExpandAll();
            }
        }

        private void ReloadMainPanel()
        {
            List<Device> devices = null;

            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                devices = ctt.Devices.Where(d => d.IsActive).ToList();
            }

            var parents = devices.Where(d => d.ParentID == null && d.IsActive).ToList();

            pnlMainShow.Controls.Clear();

            FlowLayoutPanel mainPanels = new FlowLayoutPanel();
            mainPanels.BorderStyle = BorderStyle.None;
            mainPanels.FlowDirection = FlowDirection.TopDown;
            mainPanels.AutoSize = true;

            pnlMainShow.Controls.Add(mainPanels);

            if (parents.Count > 0)
            {
                foreach (Device parent in parents)
                {
                    var children = devices.Where(d => d.ParentID == parent.DeviceID && d.IsActive && d.IsDisplay).ToList();
                    if (children.Count > 0)
                    {
                        Label lineName = new Label();
                        lineName.Text = parent.Name;
                        lineName.Font = new System.Drawing.Font(new FontFamily("宋体"), 9f, FontStyle.Bold | FontStyle.Underline);
                        lineName.Width = pnlMainShow.Width;
                        lineName.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
                        lineName.Padding = new System.Windows.Forms.Padding(5);
                        lineName.BorderStyle = BorderStyle.None;
                        lineName.BackColor = Color.LightGray;
                        lineName.TextAlign = ContentAlignment.MiddleLeft;

                        mainPanels.Controls.Add(lineName);

                        FlowLayoutPanel childPanels = new FlowLayoutPanel();
                        childPanels.BorderStyle = BorderStyle.None;
                        childPanels.FlowDirection = FlowDirection.LeftToRight;
                        childPanels.AutoSize = true;

                        foreach (Device child in children)
                        {
                            Panel childPanel = new Panel();

                            CreateMonitorPanel(childPanel, child);
                            
                            if (child.Status == null || child.Status.Value == ProtectorStatus.Normal)
                            {
                                childPanel.BackColor = Color.PaleGreen;
                            }
                            else if (child.Status.Value == ProtectorStatus.Stopped)
                            {
                                childPanel.BackColor = Color.Red;
                            }
                            else if (child.Status.Value == ProtectorStatus.Alarm)
                            {
                                childPanel.BackColor = Color.Khaki;
                            }

                            childPanels.Controls.Add(childPanel);
                            
                        }

                        mainPanels.Controls.Add(childPanels);                        
                    }
                }
            }
        }

        private void CreateMonitorPanel(Panel parent, Device device)
        {
            parent.ResumeLayout(false);
            parent.SuspendLayout();

            Button btnAlarmClear = new Button();
            Button btnReset = new Button();
            Button btnStart = new Button();
            Button btnHide = new Button();

            // Alarm Clear button
            btnAlarmClear.Location = new System.Drawing.Point(7, 201);
            btnAlarmClear.Name = "btnAlarmClear";
            btnAlarmClear.Size = new System.Drawing.Size(75, 30);
            btnAlarmClear.TabIndex = 16;
            btnAlarmClear.Text = "消除报警";
            btnAlarmClear.UseVisualStyleBackColor = true;
            btnAlarmClear.Click += mainButtonClearAlarm_Click;

            // reset button
            btnReset.Location = new System.Drawing.Point(88, 201);
            btnReset.Name = "btnReset";
            btnReset.Size = new System.Drawing.Size(75, 30);
            btnReset.TabIndex = 17;
            btnReset.Text = "复位";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += mainButtonReset_Click;

            // start button
            btnStart.Location = new System.Drawing.Point(169, 201);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(75, 30);
            btnStart.TabIndex = 18;
            btnStart.Text = "停机启动";
            btnStart.UseVisualStyleBackColor = true;

            // hide button
            btnHide.Location = new System.Drawing.Point(250, 201);
            btnHide.Name = "btnHide";
            btnHide.Size = new System.Drawing.Size(75, 30);
            btnHide.TabIndex = 19;
            btnHide.Text = "隐藏";
            btnHide.UseVisualStyleBackColor = true;
            btnHide.Click += mainButtonHide_Click;

            if (device.Status == null || device.Status.Value == ProtectorStatus.Normal)
            {
                btnStart.Enabled = false;
                btnAlarmClear.Enabled = false;
            }
            else if (device.Status.Value == ProtectorStatus.Stopped)
            {
                btnStart.Enabled = true;
                btnAlarmClear.Enabled = false;
            }
            else if (device.Status.Value == ProtectorStatus.Alarm)
            {
                btnStart.Enabled = false;
                btnAlarmClear.Enabled = true;
            }

            // title
            Label lblProtectorNameValue = new Label();
            //Label lblProtectorNameKey = new Label();
            Label lblDeviceID = new Label();

            //lblProtectorNameKey.AutoSize = true;
            //lblProtectorNameKey.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //lblProtectorNameKey.Location = new System.Drawing.Point(4, 7);
            //lblProtectorNameKey.Name = "lblProtectorNameKey";
            //lblProtectorNameKey.Size = new System.Drawing.Size(84, 12);
            //lblProtectorNameKey.TabIndex = 0;
            //lblProtectorNameKey.Text = "保护器名称: ";

            lblProtectorNameValue.AutoSize = true;
            lblProtectorNameValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lblProtectorNameValue.Location = new System.Drawing.Point(90, 6);
            lblProtectorNameValue.Name = "lblProtectorNameValue";
            lblProtectorNameValue.TabIndex = 1;
            lblProtectorNameValue.Text = device.Name + " - 保护器";

            lblDeviceID.Visible = false;
            lblDeviceID.Name = "lblDeviceID";
            lblDeviceID.Text = device.DeviceID.ToString();

            // current A B C
            Label lblCurrentCKey = new Label();
            Label lblCurrentBKey = new Label();
            Label lblCurrentAKey = new Label();
            Label lblCurrentCValue = new Label();
            Label lblCurrentBValue = new Label();
            Label lblCurrentAValue = new Label();

            lblCurrentAKey.AutoSize = true;
            lblCurrentAKey.Location = new System.Drawing.Point(6, 30);
            lblCurrentAKey.Name = "lblCurrentAKey";
            lblCurrentAKey.Size = new System.Drawing.Size(65, 12);
            lblCurrentAKey.TabIndex = 2;
            lblCurrentAKey.Text = "电流 - A: ";

            lblCurrentBKey.AutoSize = true;
            lblCurrentBKey.Location = new System.Drawing.Point(112, 30);
            lblCurrentBKey.Name = "label4";
            lblCurrentBKey.Size = new System.Drawing.Size(65, 12);
            lblCurrentBKey.TabIndex = 3;
            lblCurrentBKey.Text = "电流 - B: ";

            lblCurrentCKey.AutoSize = true;
            lblCurrentCKey.Location = new System.Drawing.Point(227, 30);
            lblCurrentCKey.Name = "label5";
            lblCurrentCKey.Size = new System.Drawing.Size(65, 12);
            lblCurrentCKey.TabIndex = 4;
            lblCurrentCKey.Text = "电流 - C: ";

            lblCurrentAValue.AutoSize = true;
            lblCurrentAValue.Location = new System.Drawing.Point(63, 30);
            lblCurrentAValue.Name = "lblCurrentAValue";
            lblCurrentAValue.Size = new System.Drawing.Size(47, 12);
            lblCurrentAValue.TabIndex = 20;
            lblCurrentAValue.Text = device.CurrentA == null ? "--" : device.CurrentA.Value.ToString("X2") + "A";

            lblCurrentBValue.AutoSize = true;
            lblCurrentBValue.Location = new System.Drawing.Point(169, 30);
            lblCurrentBValue.Name = "lblCurrentAValue";
            lblCurrentBValue.Size = new System.Drawing.Size(47, 12);
            lblCurrentBValue.TabIndex = 21;
            lblCurrentBValue.Text = device.CurrentB == null ? "--" : device.CurrentB.Value.ToString("X2") + "A";

            lblCurrentCValue.AutoSize = true;
            lblCurrentCValue.Location = new System.Drawing.Point(284, 30);
            lblCurrentCValue.Name = "lblCurrentCValue";
            lblCurrentCValue.Size = new System.Drawing.Size(47, 12);
            lblCurrentCValue.TabIndex = 22;
            lblCurrentCValue.Text = device.CurrentC == null ? "--" : device.CurrentC.Value.ToString("X2") + "A";

            // Vol A B C
            Label lblVolCKey = new Label();
            Label lblVolBKey = new Label();
            Label lblVolAKey = new Label();
            Label lblVolCValue = new Label();
            Label lblVolBValue = new Label();
            Label lblVolAValue = new Label();

            lblVolAKey.AutoSize = true;
            lblVolAKey.Location = new System.Drawing.Point(6, 53);
            lblVolAKey.Name = "lblVolAKey";
            lblVolAKey.Size = new System.Drawing.Size(65, 12);
            lblVolAKey.TabIndex = 5;
            lblVolAKey.Text = "电压 - A: ";

            lblVolBKey.AutoSize = true;
            lblVolBKey.Location = new System.Drawing.Point(112, 53);
            lblVolBKey.Name = "lblVolBKey";
            lblVolBKey.Size = new System.Drawing.Size(65, 12);
            lblVolBKey.TabIndex = 6;
            lblVolBKey.Text = "电压 - B: ";

            lblVolCKey.AutoSize = true;
            lblVolCKey.Location = new System.Drawing.Point(227, 53);
            lblVolCKey.Name = "lblVolCKey";
            lblVolCKey.Size = new System.Drawing.Size(65, 12);
            lblVolCKey.TabIndex = 7;
            lblVolCKey.Text = "电压 - C: ";

            lblVolAValue.AutoSize = true;
            lblVolAValue.Location = new System.Drawing.Point(63, 53);
            lblVolAValue.Name = "lblVolAValue";
            lblVolAValue.Size = new System.Drawing.Size(47, 12);
            lblVolAValue.TabIndex = 23;
            lblVolAValue.Text = device.VoltageA == null ? "--" : device.VoltageA.Value.ToString("X2") + "V";

            lblVolBValue.AutoSize = true;
            lblVolBValue.Location = new System.Drawing.Point(169, 53);
            lblVolBValue.Name = "lblVolBValue";
            lblVolBValue.Size = new System.Drawing.Size(47, 12);
            lblVolBValue.TabIndex = 24;
            lblVolBValue.Text = device.VoltageB == null ? "--" : device.VoltageB.Value.ToString("X2") + "V";

            lblVolCValue.AutoSize = true;
            lblVolCValue.Location = new System.Drawing.Point(284, 53);
            lblVolCValue.Name = "lblVolCValue";
            lblVolCValue.Size = new System.Drawing.Size(47, 12);
            lblVolCValue.TabIndex = 25;
            lblVolCValue.Text = device.VoltageC == null ? "--" : device.VoltageC.Value.ToString("X2") + "V";

            // Power
            Label lblPowerKey = new Label();
            Label lblPowerValue = new Label();

            lblPowerKey.AutoSize = true;
            lblPowerKey.Location = new System.Drawing.Point(6, 78);
            lblPowerKey.Name = "lblPowerKey";
            lblPowerKey.Size = new System.Drawing.Size(41, 12);
            lblPowerKey.TabIndex = 8;
            lblPowerKey.Text = "功率: ";

            lblPowerValue.AutoSize = true;
            lblPowerValue.Location = new System.Drawing.Point(41, 78);
            lblPowerValue.Name = "lblPowerValue";
            lblPowerValue.Size = new System.Drawing.Size(47, 12);
            lblPowerValue.TabIndex = 26;
            lblPowerValue.Text = device.Power == null ? "--" : device.Power.Value.ToString("X2") + "KW";

            // Alarm time
            Label lblAlarmTimeKey = new Label();
            Label lblAlarmTimeValue = new Label();

            lblAlarmTimeKey.AutoSize = true;
            lblAlarmTimeKey.Location = new System.Drawing.Point(6, 102);
            lblAlarmTimeKey.Name = "lblAlarmTimeKey";
            lblAlarmTimeKey.Size = new System.Drawing.Size(65, 12);
            lblAlarmTimeKey.TabIndex = 9;
            lblAlarmTimeKey.Text = "报警时间: ";

            lblAlarmTimeValue.AutoSize = true;
            lblAlarmTimeValue.Location = new System.Drawing.Point(63, 102);
            lblAlarmTimeValue.Name = "lblAlarmTimeValue";
            lblAlarmTimeValue.Size = new System.Drawing.Size(47, 12);
            lblAlarmTimeValue.TabIndex = 27;
            lblAlarmTimeValue.Text = device.AlarmAt == null ? "--" : device.AlarmAt.Value.ToString("yyyy-MM-dd hh:mm:ss");

            // Stop time
            Label lblStopTimeKey = new Label();
            Label lblStopTimeValue = new Label();

            lblStopTimeKey.AutoSize = true;
            lblStopTimeKey.Location = new System.Drawing.Point(6, 125);
            lblStopTimeKey.Name = "lblStopTimeKey";
            lblStopTimeKey.Size = new System.Drawing.Size(65, 12);
            lblStopTimeKey.TabIndex = 10;
            lblStopTimeKey.Text = "停机时间: ";

            lblStopTimeValue.AutoSize = true;
            lblStopTimeValue.Location = new System.Drawing.Point(63, 125);
            lblStopTimeValue.Name = "lblStopTimeValue";
            lblStopTimeValue.Size = new System.Drawing.Size(47, 12);
            lblStopTimeValue.TabIndex = 28;
            lblStopTimeValue.Text = device.StopAt == null ? "--" : device.StopAt.Value.ToString("yyyy-MM-dd hh:mm:ss");

            // Temp A B C
            Label lblTemCKey = new Label();
            Label lblTemBKey = new Label();
            Label lblTemAKey = new Label();
            Label lblTemCValue = new Label();
            Label lblTemBValue = new Label();
            Label lblTemAValue = new Label();

            lblTemCKey.AutoSize = true;
            lblTemCKey.Location = new System.Drawing.Point(227, 148);
            lblTemCKey.Name = "lblTemCKey";
            lblTemCKey.Size = new System.Drawing.Size(65, 12);
            lblTemCKey.TabIndex = 13;
            lblTemCKey.Text = "温度 - C: ";

            lblTemBKey.AutoSize = true;
            lblTemBKey.Location = new System.Drawing.Point(112, 148);
            lblTemBKey.Name = "lblTemBKey";
            lblTemBKey.Size = new System.Drawing.Size(65, 12);
            lblTemBKey.TabIndex = 12;
            lblTemBKey.Text = "温度 - B: ";

            lblTemAKey.AutoSize = true;
            lblTemAKey.Location = new System.Drawing.Point(6, 148);
            lblTemAKey.Name = "lblTemAKey";
            lblTemAKey.Size = new System.Drawing.Size(65, 12);
            lblTemAKey.TabIndex = 11;
            lblTemAKey.Text = "温度 - A: ";

            lblTemCValue.AutoSize = true;
            lblTemCValue.Location = new System.Drawing.Point(282, 148);
            lblTemCValue.Name = "lblTemCValue";
            lblTemCValue.Size = new System.Drawing.Size(47, 12);
            lblTemCValue.TabIndex = 31;
            lblTemCValue.Text = device.TemperatureC == null ? "--" : device.TemperatureC.Value.ToString("X2");
            
            lblTemBValue.AutoSize = true;
            lblTemBValue.Location = new System.Drawing.Point(167, 148);
            lblTemBValue.Name = "lblTemBValue";
            lblTemBValue.Size = new System.Drawing.Size(47, 12);
            lblTemBValue.TabIndex = 30;
            lblTemBValue.Text = device.TemperatureB == null ? "--" : device.TemperatureB.Value.ToString("X2");
            
            lblTemAValue.AutoSize = true;
            lblTemAValue.Location = new System.Drawing.Point(61, 148);
            lblTemAValue.Name = "lblTemAValue";
            lblTemAValue.Size = new System.Drawing.Size(47, 12);
            lblTemAValue.TabIndex = 29;
            lblTemAValue.Text = device.TemperatureA == null ? "--" : device.TemperatureA.Value.ToString("X2");

            // RM#1 2 status
            Label lblRM2Key = new Label();
            Label lblRM1Key = new Label();
            Label lblRM2Value = new Label();
            Label lblRM1Value = new Label();

            lblRM2Key.AutoSize = true;
            lblRM2Key.Location = new System.Drawing.Point(112, 175);
            lblRM2Key.Name = "lblRM2Key";
            lblRM2Key.Size = new System.Drawing.Size(59, 12);
            lblRM2Key.TabIndex = 15;
            lblRM2Key.Text = "继电器#2:";
             
            lblRM1Key.AutoSize = true;
            lblRM1Key.Location = new System.Drawing.Point(6, 175);
            lblRM1Key.Name = "lblRM1Key";
            lblRM1Key.Size = new System.Drawing.Size(65, 12);
            lblRM1Key.TabIndex = 14;
            lblRM1Key.Text = "继电器#1: ";

            lblRM2Value.AutoSize = true;
            lblRM2Value.Location = new System.Drawing.Point(169, 175);
            lblRM2Value.Name = "lblRM2Value";
            lblRM2Value.Size = new System.Drawing.Size(47, 12);
            lblRM2Value.TabIndex = 33;
            if (device.SecondRMStatus == null)
                lblRM2Value.Text = "--";
            else if (device.SecondRMStatus.Value)
                lblRM2Value.Text = "打开";
            else
                lblRM2Value.Text = "关闭";
            
            lblRM1Value.AutoSize = true;
            lblRM1Value.Location = new System.Drawing.Point(63, 175);
            lblRM1Value.Name = "lblRM1Value";
            lblRM1Value.Size = new System.Drawing.Size(47, 12);
            lblRM1Value.TabIndex = 32;
            if (device.FirstRMStatus == null)
                lblRM1Value.Text = "--";
            else if (device.FirstRMStatus.Value)
                lblRM1Value.Text = "打开";
            else
                lblRM1Value.Text = "关闭";

            parent.Controls.Add(lblRM1Value);
            parent.Controls.Add(lblRM2Value);
            parent.Controls.Add(lblRM1Key);
            parent.Controls.Add(lblRM2Key);
            parent.Controls.Add(lblTemAValue);
            parent.Controls.Add(lblTemBValue);
            parent.Controls.Add(lblTemCValue);
            parent.Controls.Add(lblTemAKey);
            parent.Controls.Add(lblTemBKey);
            parent.Controls.Add(lblTemCKey);
            parent.Controls.Add(lblStopTimeValue);
            parent.Controls.Add(lblStopTimeKey);
            parent.Controls.Add(lblAlarmTimeValue);
            parent.Controls.Add(lblAlarmTimeKey);
            parent.Controls.Add(lblPowerValue);
            parent.Controls.Add(lblPowerKey);
            parent.Controls.Add(lblVolAValue);
            parent.Controls.Add(lblVolBValue);
            parent.Controls.Add(lblVolCValue);
            parent.Controls.Add(lblVolAKey);
            parent.Controls.Add(lblVolBKey);
            parent.Controls.Add(lblVolCKey);
            parent.Controls.Add(lblCurrentAValue);
            parent.Controls.Add(lblCurrentBValue);
            parent.Controls.Add(lblCurrentCValue);
            parent.Controls.Add(lblCurrentAKey);
            parent.Controls.Add(lblCurrentBKey);
            parent.Controls.Add(lblCurrentCKey);
            //parent.Controls.Add(lblProtectorNameKey);
            parent.Controls.Add(lblProtectorNameValue);
            parent.Controls.Add(lblDeviceID);
            parent.Controls.Add(btnAlarmClear);
            parent.Controls.Add(btnReset);
            parent.Controls.Add(btnStart);
            parent.Controls.Add(btnHide);
            parent.Name = "parent";
            parent.Size = new System.Drawing.Size(334, 239);
            parent.TabIndex = 0;
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
