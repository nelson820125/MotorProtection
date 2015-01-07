using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MotorProtection.Core.Crypto;
using MotorProtection.Core.Data.Entities;

namespace MotorProtection.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMsg.Text = "Please input username and password";
                return;
            }
            else
            {
                password = CryptoUtils.ComputeHash(password);

                // verify user
                bool isValid = true;
                using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                {
                    var user = ctt.Users.Where(u => u.UserName == username && u.Password == password && u.Enabled).FirstOrDefault();
                    if (user == null)
                    {
                        isValid = false;
                    }
                }

                if (isValid)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();                    
                }
                else
                {
                    lblMsg.Text = "Username or password is invalid.";
                    return;
                }
            }
        }
    }
}
