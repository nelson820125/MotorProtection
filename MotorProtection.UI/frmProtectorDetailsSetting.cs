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
        private byte[] READ_START_ADDRESS = new byte[] { RegisterAddresses.ProtectorSettingHi, RegisterAddresses.ProtectorPowerSettingLo };
        private byte[] READ_REGISTER_NUM = new byte[] { 0x00, 0x07 };

        private int START_ADDRESS = 40001;
        private int BYTE_NUM = 14;
        private int REG_NUM = 7;

        private DeviceConfig _config = new DeviceConfig();
        private ServiceController _serviceCtrl = null;

        public frmProtectorDetailsSetting()
        {
            InitializeComponent();
        }

        public frmProtectorDetailsSetting(DeviceConfig config)
        {
            InitializeComponent();
            _config = config;
        }

        #region events

        private void frmProtectorDetailsSetting_Load(object sender, EventArgs e)
        {
            _serviceCtrl = Controller.WinServiceController.GetServiceByName(JobManagerKey.JOB_NAME);

            InitializeSyncButton();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnReadProtector_Click(object sender, EventArgs e)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                DeviceConfigurationPool pool = new DeviceConfigurationPool()
                {
                    Address = _config.Device.Address.Value,
                    FunCode = FunctionCodes.READ_REGISTERS,
                    Commands = ParsingReadCommands(),
                    Description = "",
                    UserID = 1,
                    CreateTime = DateTime.Now,
                    Attempt = 0,
                    Status = ConfigurationStatus.PROCESSING
                };

                ctt.DeviceConfigurationPools.AddObject(pool);
                ctt.SaveChanges();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            //{
            //    DeviceConfigurationPool pool = new DeviceConfigurationPool()
            //    {
            //        Address = _config.Device.Address.Value,
            //        FunCode = FunctionCodes.READ_REGISTERS,
            //        Commands = ParsingWriteCommands(),
            //        Description = "",
            //        UserID = 1,
            //        CreateTime = DateTime.Now,
            //        Attempt = 0,
            //        Status = ConfigurationStatus.PROCESSING
            //    };

            //    ctt.DeviceConfigurationPools.AddObject(pool);
            //    ctt.SaveChanges();
            //}

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
        }

        #endregion

        #region private

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
                }
                else
                {
                    btnReadProtector.Enabled = true;
                }
            }
        }

        #endregion
    }
}
