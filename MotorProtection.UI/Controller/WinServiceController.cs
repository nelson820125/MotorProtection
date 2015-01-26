using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MotorProtection.UI.Controller
{
    public class WinServiceController
    {
        public static ServiceController GetServiceByName(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                if (service.ServiceName.ToLower() == serviceName.ToLower())
                {
                    return service;
                }
            }
            return null;
        }
    }
}
