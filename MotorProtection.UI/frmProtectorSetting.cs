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
    public partial class frmProtectorSetting : Form
    {
        private OperationKey _oper = OperationKey.Add;
        private Device _device = null;
        private string _lineName;

        public frmProtectorSetting()
        {
            InitializeComponent();
        }

        public frmProtectorSetting(OperationKey oper, Device device, string lineName)
        {
            InitializeComponent();
            _oper = oper;
            _device = device;
            _lineName = lineName;
        }

        #region events

        private void frmProtectorSetting_Load(object sender, EventArgs e)
        {
            InitializeComp();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var protectorName = txtProtectorName.Text.Trim();
            var protectorAddr = txtProtectorAddress.Text.Trim();
            int addr = 0;
            var parentName = cbxLineName.Text;

            if (!string.IsNullOrEmpty(protectorName) && !string.IsNullOrEmpty(protectorAddr) && Int32.TryParse(protectorAddr, out addr) && !string.IsNullOrEmpty(parentName))
            {
                if (_oper == OperationKey.Add)
                    AddProtectorValidation(protectorName, addr, Convert.ToInt32(cbxLineName.SelectedValue), cbxLineName.SelectedText);
                else if (_oper == OperationKey.Edit)
                    EditProtectorValidation(protectorName, addr, Convert.ToInt32(cbxLineName.SelectedValue), cbxLineName.SelectedText);
                CacheController.UpdateAllCacheGroupTimestamp();
            }
            else
            {
                InputsValidation();
            }
        }

        #endregion

        #region private

        private void InitializeComp()
        {
            if (_oper == OperationKey.Add)
                this.Text = "添加保护器信息";
            else if (_oper == OperationKey.Edit)
                this.Text = "编辑保护器信息";

            LineListBind(GetLines());
        }

        private List<Device> GetLines()
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                return ctt.Devices.Where(d => d.ParentID == null && d.IsActive).ToList();
            }
        }

        private void LineListBind(List<Device> devices)
        {
            cbxLineName.Items.Clear();
            if (devices.Count > 0)
            {
                cbxLineName.DataSource = devices;
                cbxLineName.DisplayMember = "Name";
                cbxLineName.ValueMember = "DeviceID";

                if (_oper == OperationKey.Add)
                {
                    if (!string.IsNullOrEmpty(_lineName))
                        cbxLineName.Text = _lineName;
                    else
                        cbxLineName.SelectedIndex = 0;
                }
                else if (_oper == OperationKey.Edit)
                {
                    cbxLineName.SelectedValue = _device.ParentID; 
                }
            }
        }

        private void InputsValidation()
        {
            var protectorName = txtProtectorName.Text.Trim();
            var protectorAddr = txtProtectorAddress.Text.Trim();
            int addr = 0;
            var parentName = cbxLineName.Text;

            if (string.IsNullOrEmpty(parentName))
            {
                MessageBox.Show("请选择所属的生产线");
                cbxLineName.Focus();
            }
            else if (string.IsNullOrEmpty(protectorName))
            {
                MessageBox.Show("请填写保护器名称");
                txtProtectorName.Focus();
            }
            else if (string.IsNullOrEmpty(protectorAddr))
            {
                MessageBox.Show("请填写保护器地址");
                txtProtectorAddress.Focus();
            }
            else if (!Int32.TryParse(protectorAddr, out addr))
            {
                MessageBox.Show("保护器地址只允许添加数字");
                txtProtectorAddress.Focus();
            }
        }

        /// <summary>
        /// Verify the inputs if add protector information
        /// </summary>
        private void AddProtectorValidation(string protectorName, int address, int parentId, string lineName)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                // verify if address is exist
                var protector = ctt.Devices.Where(d => d.IsActive && d.Address == address).FirstOrDefault();
                if (protector != null && rbtnActive.Checked)
                {
                    MessageBox.Show("已经存在相同地址的保护器被启用，请更换保护器的地址");
                    txtProtectorAddress.Focus();
                }
                else
                {
                    // verify if the name is exist
                    protector = ctt.Devices.Where(d => d.Name == protectorName && d.IsActive && d.ParentID == parentId).FirstOrDefault();
                    if (protector != null && rbtnActive.Checked)
                    {
                        MessageBox.Show("此生产线上已经存在相同名字的保护器被启用，请更换保护器的名字或所属的生产线");
                        txtProtectorName.Focus();
                    }
                    else
                    {
                        Device device = new Device()
                        {
                            Name = protectorName,
                            Address = address,
                            ParentID = parentId,
                            CreateTime = DateTime.Now,
                            IsActive = rbtnActive.Checked
                        };
                        ctt.Devices.AddObject(device);
                        ctt.SaveChanges();

                        LogController.LogEvent(AuditingLevel.High).Add("Description", string.Format("User ID: {0} add new device at line{1}, name is {2} and created at {3}.", "1", lineName, protectorName, device.CreateTime.ToString())).Write();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
            }
        }

        /// <summary>
        /// Verify the inputs if edit protector information
        /// </summary>
        private void EditProtectorValidation(string protectorName, int address, int parentId, string lineName)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                // verify if address is exist
                var protector = ctt.Devices.Where(d => d.IsActive && d.Address == address).FirstOrDefault();
                if (protector != null && protector.DeviceID != _device.DeviceID && rbtnActive.Checked)
                {
                    MessageBox.Show("已经存在相同地址的保护器被启用，请更换保护器的地址");
                    txtProtectorAddress.Focus();
                }
                else
                {
                    // verify if the name is exist
                    protector = ctt.Devices.Where(d => d.Name == protectorName && d.IsActive && d.ParentID == parentId).FirstOrDefault();
                    if (protector != null && protector.DeviceID != _device.DeviceID && rbtnActive.Checked)
                    {
                        MessageBox.Show("此生产线上已经存在相同名字的保护器被启用，请更换保护器的名字或所属的生产线");
                        txtProtectorName.Focus();
                    }
                    else
                    {
                        protector.Name = protectorName;
                        protector.Address = address;
                        protector.ParentID = parentId;
                        protector.UpdateTime = DateTime.Now;
                        
                        ctt.SaveChanges();

                        LogController.LogEvent(AuditingLevel.High).Add("Description", string.Format("User ID: {0} edit the device at line{1}, ID is {1} and updated at {2}.", "1", lineName, protector.DeviceID, protector.UpdateTime.ToString())).Write();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
            }
        }

        #endregion
    }
}
