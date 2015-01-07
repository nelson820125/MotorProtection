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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void tsmiAboutSystem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("电机保护器控制软件V1.0\n技术支持：李宁");
        }

        private void tmsiSignOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
