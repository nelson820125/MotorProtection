using MotorProtection.Constant;
using MotorProtection.Core.Controller;
using MotorProtection.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MotorProtection.UI
{
    public partial class frmMessage : Form
    {
        private ServiceController _service = null;
        private JobOperation _operation;
        private bool _hasOperation = false;
        private DeviceConfigurationPool _pool = null;

        public frmMessage()
        {
            InitializeComponent();
            _hasOperation = false;
        }

        public frmMessage(string msg)
        {
            InitializeComponent();
            lblMsg.Text = msg;
            _hasOperation = false;
        }

        public frmMessage(string msg, ServiceController servive, JobOperation oper)
        {
            InitializeComponent();
            lblMsg.Text = msg;
            _service = servive;
            _operation = oper;
            _hasOperation = true;
        }

        public frmMessage(string msg, DeviceConfigurationPool pool)
        {
            InitializeComponent();
            lblMsg.Text = msg;
            _pool = pool;
            _operation = JobOperation.None;
            _hasOperation = true;
        }

        private void VerifyStatus()
        {
            if (_operation == JobOperation.Start)
            {
                if (_service == null)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
                else
                {
                    while (true)
                    {
                        if (_service.Status == ServiceControllerStatus.Running)
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            break;
                        }
                    }
                }
            }
            else if (_operation == JobOperation.Stop)
            {
                if (_service == null)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
                else
                {
                    while (true)
                    {
                        if (_service.Status == ServiceControllerStatus.Stopped)
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            break;
                        }                            
                    }
                }
            }
            else if (_operation == JobOperation.None && _pool != null) // deal the operation from high level
            {
                bool isSuccess = false;
                using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                {
                    while (true)
                    {
                        var pool = ctt.DeviceConfigurationPools.Where(dcp =>dcp.ID == _pool.ID).FirstOrDefault();
                        if (pool.Status == ConfigurationStatus.PROCESSING)
                        {
                            Thread.Sleep(1000);
                            continue;
                        }
                        else {
                            if (pool.Status == ConfigurationStatus.SUCCESS)
                                isSuccess = true;
                            else if (pool.Status == ConfigurationStatus.ERROR)
                                isSuccess = false;
                            break;
                        }
                    }
                }

                DeviceConfigsController ctrl = new DeviceConfigsController();
                ctrl.UpdatePoolAfterSuccess(_pool.ID);

                if (isSuccess)
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                else
                    this.DialogResult = System.Windows.Forms.DialogResult.No;
            }

            Thread.CurrentThread.Abort();
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {
            if (_hasOperation)
            {
                Thread verifyThread = new Thread(new ThreadStart(VerifyStatus));
                verifyThread.Start();
            }
            else
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.Text = "系统提示";
                this.picLoading.Image = Image.FromFile(@"images\icon\32x32\attention.gif");
                this.ShowIcon = false;
            }
        }
    }
}
