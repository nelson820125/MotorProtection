using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MotorProtection.Core.Crypto;

namespace PasswordGenerator
{
    public partial class frmPWDGwnerator : Form
    {
        public frmPWDGwnerator()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var pwd = txtPwd.Text.Trim();

            if (!string.IsNullOrEmpty(pwd))
            {
                var newPwd = CryptoUtils.ComputeHash(pwd);
                txtNewPwd.Text = newPwd;
            }
        }
    }
}
