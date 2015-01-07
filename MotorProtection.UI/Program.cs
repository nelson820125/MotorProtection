using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MotorProtection.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmLogin login = new frmLogin();
            login.ShowDialog();
            if (login.DialogResult == DialogResult.OK)
            {
                Application.Run(new frmMain());
            }
            else
            {
                return;
            }
        }
    }
}
