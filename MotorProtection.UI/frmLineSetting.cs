using MotorProtection.Core.Cache;
using MotorProtection.Core.Data.Entities;
using MotorProtection.Core.Log;
using MotorProtection.UI.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MotorProtection.UI
{
    public partial class frmLineSetting : Form
    {
        private OperationKey _oper = OperationKey.Add;
        private Device _device = null;

        public frmLineSetting()
        {
            InitializeComponent();
        }

        public frmLineSetting(OperationKey oper, Device device)
        {
            InitializeComponent();
            _oper = oper;
            _device = device;
        }

        private void InitializeComp()
        {
            if (_oper == OperationKey.Add)
            {
                this.Text = "添加生产线信息";
            }
            else if (_oper == OperationKey.Edit)
            { 
                this.Text = "编辑生产线信息";
                BindData();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var lineName = txtLineName.Text.Trim();
            
            if (!string.IsNullOrEmpty(lineName))
            {
                if (_oper == OperationKey.Add)
                    AddLineValidation(lineName);
                else if (_oper == OperationKey.Edit)
                    EditLineValidation(lineName);
                CacheController.UpdateAllCacheGroupTimestamp();                
            }
            else
            {
                MessageBox.Show("请填入生产线的名称");
                txtLineName.Focus();
            }
        }

        private void frmLineSetting_Load(object sender, EventArgs e)
        {
            InitializeComp();
        }

        #region private

        /// <summary>
        /// Verify the inputs if add line information
        /// </summary>
        /// <param name="lineName"></param>
        private void AddLineValidation(string lineName)
        {
            // verify if the line's name is exist
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var line = ctt.Devices.Where(d => d.Name == lineName && d.IsActive).FirstOrDefault();

                if (line != null && rbtnActive.Checked)
                {
                    MessageBox.Show("已经存在相同名字的生产线被启用，请更换生产线名称或状态");
                    txtLineName.Focus();
                }
                else
                {
                    Device device = new Device() { 
                        Name = lineName,
                        IsActive = rbtnActive.Checked,
                        CreateTime = DateTime.Now
                    };
                    ctt.Devices.AddObject(device);
                    ctt.SaveChanges();

                    LogController.LogEvent(AuditingLevel.High).Add("Description", string.Format("User ID: {0} add new device, name is {1} and created at {2}.", "1", lineName, device.CreateTime.ToString())).Write();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// Verify the inputs if edit line information
        /// </summary>
        /// <param name="lineName"></param>
        private void EditLineValidation(string lineName)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var line = ctt.Devices.Where(d => d.Name == lineName && d.IsActive).FirstOrDefault();

                if (line != null && _device.DeviceID != line.DeviceID && rbtnActive.Checked)
                {
                    MessageBox.Show("已经存在相同名字的生产线被启用，请更换生产线名称或状态");
                    txtLineName.Focus();
                }
                else
                {
                    line.Name = lineName;
                    line.IsActive = rbtnActive.Checked;
                    line.UpdateTime = DateTime.Now;
                    ctt.SaveChanges();

                    LogController.LogEvent(AuditingLevel.High).Add("Description", string.Format("User ID: {0} edit the device, ID is {1} and updated at {2}.", "1", line.DeviceID.ToString(), line.UpdateTime.ToString())).Write();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private void BindData()
        {
            if (_device != null)
            {
                txtLineName.Text = _device.Name;

                if (_device.IsActive)
                    rbtnActive.Checked = true;
                else
                    rbtnDeactive.Checked = true;
            }
        }

        #endregion
    }
}
