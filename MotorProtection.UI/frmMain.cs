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
    }
}
