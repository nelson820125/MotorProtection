using MotorProtection.Constant;
using MotorProtection.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace MotorProtection.UI
{
    public partial class frmProtectorDetailsSetting : Form
    {
        private byte[] READ_START_ADDRESS = new byte[] { RegisterAddresses.ProtectorSettingHi, RegisterAddresses.ProtectorStatusResetLo };
        private byte[] READ_REGISTER_NUM = new byte[] { 0x00, 0x08 };

        private DeviceConfig _config = new DeviceConfig();
        private ServiceController _serviceCtrl = null;
        private string _addressID;

        private int START_ADDRESS = 40001;
        private int BYTE_NUM = 14;
        private int REG_NUM = 7;

        public frmProtectorDetailsSetting()
        {
            InitializeComponent();
        }

        public frmProtectorDetailsSetting(DeviceConfig config, string address)
        {
            InitializeComponent();
            _config = config == null ? new DeviceConfig() : config;
            _addressID = address;
        }

        #region events

        private void frmProtectorDetailsSetting_Load(object sender, EventArgs e)
        {
            _serviceCtrl = Controller.WinServiceController.GetServiceByName(JobManagerKey.JOB_NAME);

            InitializeSyncButton();
            BindFields();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnReadProtector_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_addressID))
            {
                MessageBox.Show("还没有正确配置设备地址，无法读取设备信息");
            }
            else
            {
                DeviceConfigurationPool pool = new DeviceConfigurationPool();
                using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                {
                    pool.Address = Convert.ToInt32(_addressID);
                    pool.FunCode = FunctionCodes.READ_REGISTERS;
                    pool.Commands = ParsingReadCommands();
                    pool.Description = "Read protect parameters from sliver";
                    pool.UserID = 1;
                    pool.CreateTime = DateTime.Now;
                    pool.Attempt = 0;
                    pool.Status = ConfigurationStatus.PROCESSING;
                    pool.JobRemovable = false;

                    ctt.DeviceConfigurationPools.AddObject(pool);
                    ctt.SaveChanges();
                }

                frmMessage message = new frmMessage("正在读取设备设置信息，请稍后...", pool);
                message.ShowDialog();
                if (message.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                    {
                        int id = Convert.ToInt32(_addressID);
                        var configLog = ctt.DeviceConfigsLogs.Where(dcl => dcl.ID == id).FirstOrDefault();
                        BindFields(configLog);
                        ctt.DeviceConfigsLogs.DeleteObject(configLog);
                        ctt.SaveChanges();
                    }
                    message.Close();
                }
                else if (message.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    MessageBox.Show("读取信息失败，请重试或联系管理员");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _config.ProtectPower = Convert.ToDecimal(txtProtectPower.Text.Trim());

            if (rbtnCurrentMode.Checked)
                _config.ProtectMode = 0;
            else if (rbtnPowerMode.Checked)
                _config.ProtectMode = 1;

            _config.MIRatio = Convert.ToInt32(txtMRI.Text.Trim());

            _config.AlarmThreshold = Convert.ToInt32(txtAlarmGate.Text.Trim());

            _config.StopThreshold = Convert.ToInt32(txtStopGate.Text.Trim());

            if (rbtnAlarmClose.Checked)
                _config.FirstRMMode = 1;
            else if (rbtnAlarmOpen.Checked)
                _config.FirstRMMode = 2;
            else if (rbtnStopClose.Checked)
                _config.FirstRMMode = 3;
            else if (rbtnStopOpen.Checked)
                _config.FirstRMMode = 4;

            if (rbtnAlarmClose1.Checked)
                _config.SecondRMMode = 1;
            else if (rbtnAlarmOpen1.Checked)
                _config.SecondRMMode = 2;
            else if (rbtnStopClose1.Checked)
                _config.SecondRMMode = 3;
            else if (rbtnStopOpen1.Checked)
                _config.SecondRMMode = 4;

            DeviceConfigurationPool pool = new DeviceConfigurationPool();
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                // sync to silver commands
                pool.Address = Convert.ToInt32(_addressID);
                pool.FunCode = FunctionCodes.WRITE_MULTI_REGITERS;
                pool.Commands = ParsingWriteCommands();
                pool.Description = "Write protect parameters to sliver";
                pool.UserID = 1;
                pool.CreateTime = DateTime.Now;
                pool.Attempt = 0;
                pool.Status = ConfigurationStatus.PROCESSING;
                pool.JobRemovable = false;

                ctt.DeviceConfigurationPools.AddObject(pool);
                ctt.SaveChanges();
            }

            frmMessage message = new frmMessage("正在将设置写入设备，请稍后...", pool);
            message.ShowDialog();
            if (message.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                {
                    int id = Convert.ToInt32(_addressID);
                    var configLog = ctt.DeviceConfigsLogs.Where(dcl => dcl.ID == id).FirstOrDefault();
                    if (configLog != null)
                        ctt.DeviceConfigsLogs.DeleteObject(configLog);
                    ctt.SaveChanges();
                }
                message.Dispose();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else if (message.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                MessageBox.Show("写入信息失败，请重试或联系管理员");
            }
        }

        #endregion

        #region private

        private void BindFields()
        {
            if (_config != null)
            {
                txtProtectPower.Text = _config.ProtectPower == null ? "" : _config.ProtectPower.Value.ToString("G");

                if (_config.ProtectMode == null || _config.ProtectMode.Value == 0) // current
                    rbtnCurrentMode.Checked = true;
                else
                    rbtnPowerMode.Checked = true;

                txtMRI.Text = _config.MIRatio == null ? "" : _config.MIRatio.Value.ToString();
                txtAlarmGate.Text = _config.AlarmThreshold == null ? "" : _config.AlarmThreshold.Value.ToString();
                txtStopGate.Text = _config.StopThreshold == null ? "" : _config.StopThreshold.Value.ToString();

                if (_config.FirstRMMode == null || _config.FirstRMMode.Value == 1)
                    rbtnAlarmClose.Checked = true;
                else if (_config.FirstRMMode.Value == 2)
                    rbtnAlarmOpen.Checked = true;
                else if (_config.FirstRMMode.Value == 3)
                    rbtnStopClose.Checked = true;
                else if (_config.FirstRMMode.Value == 4)
                    rbtnStopOpen.Checked = true;

                if (_config.SecondRMMode == null || _config.SecondRMMode.Value == 4)
                    rbtnStopOpen1.Checked = true;
                else if (_config.SecondRMMode.Value == 2)
                    rbtnAlarmOpen1.Checked = true;
                else if (_config.SecondRMMode.Value == 3)
                    rbtnStopClose1.Checked = true;
                else if (_config.SecondRMMode.Value == 1)
                    rbtnAlarmClose1.Checked = true;
            }
        }

        private void BindFields(DeviceConfigsLog configLog)
        {
            if (configLog != null)
            {
                txtProtectPower.Text = configLog.ProtectPower == null ? "" : configLog.ProtectPower.Value.ToString("G");

                if (configLog.ProtectMode == null || configLog.ProtectMode.Value == 0) // current
                    rbtnCurrentMode.Checked = true;
                else
                    rbtnPowerMode.Checked = true;

                txtMRI.Text = configLog.MIRatio == null ? "" : configLog.MIRatio.Value.ToString();
                txtAlarmGate.Text = configLog.AlarmThreshold == null ? "" : configLog.AlarmThreshold.Value.ToString();
                txtStopGate.Text = configLog.StopThreshold == null ? "" : configLog.StopThreshold.Value.ToString();

                if (configLog.FirstRMMode == null || configLog.FirstRMMode.Value == 1)
                    rbtnAlarmClose.Checked = true;
                else if (configLog.FirstRMMode.Value == 2)
                    rbtnAlarmOpen.Checked = true;
                else if (configLog.FirstRMMode.Value == 3)
                    rbtnStopClose.Checked = true;
                else if (configLog.FirstRMMode.Value == 4)
                    rbtnStopOpen.Checked = true;

                if (configLog.SecondRMMode.Value == 1)
                    rbtnAlarmClose1.Checked = true;
                else if (configLog.SecondRMMode.Value == 2)
                    rbtnAlarmOpen1.Checked = true;
                else if (configLog.SecondRMMode.Value == 3)
                    rbtnStopClose1.Checked = true;
                else if (configLog.SecondRMMode == null || configLog.SecondRMMode.Value == 4)
                    rbtnStopOpen1.Checked = true;
            }
        }

        private void InputsValidation()
        {
            // verify if the information is complete
            EmptyValidation();

            PowerValidation();
            MIRioValidation();
            AlarmGateValidation();
            StopGateValidation();
        }

        /// <summary>
        /// verify the informance is complete
        /// </summary>
        private void EmptyValidation()
        {
            if (string.IsNullOrEmpty(txtProtectPower.Text.Trim()))
            {
                MessageBox.Show("请填写保护功率");
                txtProtectPower.Focus();
            }
            else if (string.IsNullOrEmpty(txtMRI.Text.Trim()))
            {
                MessageBox.Show("请填写互感器变比");
                txtMRI.Focus();
            }
            else if (string.IsNullOrEmpty(txtAlarmGate.Text.Trim()))
            {
                MessageBox.Show("请填写报警阈值");
                txtAlarmGate.Focus();
            }
            else if (string.IsNullOrEmpty(txtProtectPower.Text.Trim()))
            {
                MessageBox.Show("请填写停机阈值");
                txtProtectPower.Focus();
            }
        }

        /// <summary>
        /// verify the format of power
        /// </summary>
        private void PowerValidation()
        {
            int power = 0;
            if (Int32.TryParse(txtProtectPower.Text.Trim(), out power))
            {
                if (power < 0 || power > 500)
                {
                    MessageBox.Show("保护电机功率只接受0 - 500之间的整数，请重新填写");
                    txtProtectPower.Focus();
                }
            }
            else
            {
                MessageBox.Show("保护电机功率只允许填入数字，请重新填写");
                txtProtectPower.Focus();
            }
        }

        /// <summary>
        /// verify MIRio
        /// </summary>
        private void MIRioValidation()
        {
            int mir = 0;
            if (!Int32.TryParse(txtMRI.Text.Trim(), out mir))
            {
                MessageBox.Show("互感器变比只允许填入整数，请重新填写");
                txtMRI.Focus();
            }
            else
            {
                if (mir < 0)
                {
                    MessageBox.Show("互感器变比只允许填入正整数，请重新填写");
                    txtMRI.Focus();
                }
            }
        }

        /// <summary>
        /// verify alarm threshold
        /// </summary>
        private void AlarmGateValidation()
        {
            int alarmGate = 0;
            if (Int32.TryParse(txtAlarmGate.Text.Trim(), out alarmGate))
            {
                if (alarmGate < 0 || alarmGate > 100)
                {
                    MessageBox.Show("报警阈值只接受0 - 100之间的整数，请重新填写");
                    txtAlarmGate.Focus();
                }
            }
            else
            {
                MessageBox.Show("报警阈值只允许填入数字，请重新填写");
                txtAlarmGate.Focus();
            }
        }

        /// <summary>
        /// verify stop threshold
        /// </summary>
        private void StopGateValidation()
        {
            int stopGate = 0;
            if (Int32.TryParse(txtStopGate.Text.Trim(), out stopGate))
            {
                if (stopGate < 0 || stopGate > 100)
                {
                    MessageBox.Show("停机阈值只接受0 - 100之间的整数，请重新填写");
                    txtStopGate.Focus();
                }
            }
            else
            {
                MessageBox.Show("停机阈值只允许填入数字，请重新填写");
                txtStopGate.Focus();
            }
        }

        private string ParsingReadCommands()
        {
            string command = "";

            StringBuilder builder = new StringBuilder();
            builder.Clear();

            foreach (byte addr in READ_START_ADDRESS)
            {
                if (builder.Length != 0)
                    builder.Append(" ");
                builder.Append(addr.ToString("X2"));
            }

            foreach (byte o in READ_REGISTER_NUM)
            {
                if (builder.Length != 0)
                    builder.Append(" ");
                builder.Append(o.ToString("X2"));
            }

            command = builder.ToString();

            return command;
        }

        private string ParsingWriteCommands()
        {
            string command = "";

            StringBuilder builder = new StringBuilder();
            builder.Clear();

            byte[] startAddr = BitConverter.GetBytes(START_ADDRESS);
            Array.Reverse(startAddr);

            byte[] regNum = BitConverter.GetBytes(REG_NUM);
            Array.Reverse(regNum);

            byte[] byteNum = BitConverter.GetBytes(BYTE_NUM);
            Array.Reverse(byteNum);

            byte[] power = BitConverter.GetBytes(Convert.ToInt32(_config.ProtectPower.Value * 100));
            Array.Reverse(power);

            byte[] proMode = BitConverter.GetBytes(_config.ProtectMode.Value);
            Array.Reverse(proMode);

            byte[] mir = BitConverter.GetBytes(_config.MIRatio.Value);
            Array.Reverse(mir);

            byte[] alarmThreshold = BitConverter.GetBytes(_config.AlarmThreshold.Value);
            Array.Reverse(alarmThreshold);

            byte[] stopThreshold = BitConverter.GetBytes(Convert.ToInt32(_config.StopThreshold.Value));
            Array.Reverse(stopThreshold);

            byte[] mr1Mode = BitConverter.GetBytes(_config.FirstRMMode.Value);
            Array.Reverse(mr1Mode);

            byte[] mr2Mode = BitConverter.GetBytes(_config.SecondRMMode.Value);
            Array.Reverse(mr2Mode);

            builder.Append(" " + startAddr[2].ToString("X2") + " " + startAddr[3].ToString("X2"));
            builder.Append(" " + regNum[2].ToString("X2") + " " + regNum[3].ToString("X2"));
            builder.Append(" " + byteNum[3].ToString("X2"));
            builder.Append(" " + power[2].ToString("X2") + " " + power[3].ToString("X2"));
            builder.Append(" " + proMode[2].ToString("X2") + " " + proMode[3].ToString("X2"));
            builder.Append(" " + mir[2].ToString("X2") + " " + mir[3].ToString("X2"));
            builder.Append(" " + alarmThreshold[2].ToString("X2") + " " + alarmThreshold[3].ToString("X2"));
            builder.Append(" " + stopThreshold[2].ToString("X2") + " " + stopThreshold[3].ToString("X2"));
            builder.Append(" " + mr1Mode[2].ToString("X2") + " " + mr1Mode[3].ToString("X2"));
            builder.Append(" " + mr2Mode[2].ToString("X2") + " " + mr2Mode[3].ToString("X2"));

            command = builder.ToString().Trim();

            return command;
        }

        private void InitializeSyncButton()
        {
            if (_serviceCtrl == null)
            {
                btnReadProtector.Enabled = false;
            }
            else
            {
                if (_serviceCtrl.Status == ServiceControllerStatus.Paused || _serviceCtrl.Status == ServiceControllerStatus.Stopped)
                {
                    btnReadProtector.Enabled = false;
                    btnSave.Enabled = false;
                }
                else
                {
                    btnReadProtector.Enabled = true;
                    btnSave.Enabled = true;
                }
            }
        }

        #endregion
    }
}
