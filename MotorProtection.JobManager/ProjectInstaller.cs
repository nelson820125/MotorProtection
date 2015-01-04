using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace MotorProtection.JobManager
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            try
            {
                //Open the HKEY_LOCAL_MACHINE\SYSTEM key
                Microsoft.Win32.RegistryKey system = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System");
                //Open CurrentControlSet
                Microsoft.Win32.RegistryKey currentControlSet = system.OpenSubKey("CurrentControlSet");
                //Go to the services key
                Microsoft.Win32.RegistryKey services = currentControlSet.OpenSubKey("Services");
                //Open the key for your service, and allow writing
                Microsoft.Win32.RegistryKey service = services.OpenSubKey(this.serviceInstaller1.ServiceName, true);
                //Add your service's description as a REG_SZ value named "Description"
                service.SetValue("Description", "Motor Protector Job Manager.");
                //(Optional) Add some custom information your service will use...
                //Microsoft.Win32.RegistryKey config = service.CreateSubKey("Parameters");
            }
            catch { }
        }
    }
}
