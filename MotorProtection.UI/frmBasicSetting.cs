using MotorProtection.Core.Data.Entities;
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
using MotorProtection.Core;
using System.Media;
using MotorProtection.Core.Log;
using MotorProtection.Core.Cache;

namespace MotorProtection.UI
{
    public partial class frmBasicSetting : Form
    {
        public frmBasicSetting()
        {
            InitializeComponent();
        }

        private void Initialize()
        {
            BindComputerComms();
            cbxPortName.SelectedItem = AppConfig.SerialComm_PortName;
            cbxBaydRate.SelectedItem = AppConfig.SerialComm_BaudRate;
            txtAttempts.Text = AppConfig.SerialComm_Attempts.ToString();
        }

        /// <summary>
        /// Scan the available serial port on PC, then bind to cbxPortName
        /// </summary>
        private void BindComputerComms()
        {
            Microsoft.VisualBasic.Devices.Computer cmbCOM = new Microsoft.VisualBasic.Devices.Computer();

            if (cmbCOM.Ports.SerialPortNames.Count > 0)
            {
                foreach (string s in cmbCOM.Ports.SerialPortNames)
                {
                    cbxPortName.Items.Add(s);
                }

                cbxPortName.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var systemConfigs = ctt.SystemConfigs.ToList();

                var portName = systemConfigs.Where(sc => sc.Name == "SerialComm_PortName").FirstOrDefault();
                if (portName != null)
                {
                    portName.Value = cbxPortName.SelectedText;
                }
                else
                {
                    SystemConfig config = new SystemConfig()
                    {
                        Name = "SerialComm_PortName",
                        Value = cbxPortName.SelectedText
                    };
                    ctt.SystemConfigs.AddObject(config);
                }

                var braudRate = systemConfigs.Where(sc => sc.Name == "SerialComm_BaudRate").FirstOrDefault();
                if (portName != null)
                {
                    portName.Value = cbxBaydRate.SelectedText;
                }
                else
                {
                    SystemConfig config = new SystemConfig()
                    {
                        Name = "SerialComm_BaudRate",
                        Value = cbxBaydRate.SelectedText
                    };
                    ctt.SystemConfigs.AddObject(config);
                }

                var attempts = systemConfigs.Where(sc => sc.Name == "SerialComm_Attempts").FirstOrDefault();
                if (portName != null)
                {
                    portName.Value = txtAttempts.Text.Trim();
                }
                else
                {
                    SystemConfig config = new SystemConfig()
                    {
                        Name = "SerialComm_Attempts",
                        Value = txtAttempts.Text.Trim()
                    };
                    ctt.SystemConfigs.AddObject(config);
                }

                ctt.SaveChanges();
            }

            CacheController.UpdateAllCacheGroupTimestamp();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisableComponent()
        {
            cbxPortName.Enabled = false;
            cbxPortName.Enabled = false;
            txtAttempts.Enabled = false;
            btnSave.Enabled = false;
        }

        private void EnableComponent()
        {
            cbxPortName.Enabled = true;
            cbxPortName.Enabled = true;
            txtAttempts.Enabled = true;
            btnSave.Enabled = true;
        }

        private void frmBasicSetting_Load(object sender, EventArgs e)
        {
            Initialize();
            var service = Controller.WinServiceController.GetServiceByName(JobManagerKey.JOB_NAME);
            if (service == null || service.Status == ServiceControllerStatus.Stopped || service.Status == ServiceControllerStatus.Paused)
                EnableComponent();
            else
                DisableComponent();
        }
    }
}
