using MotorProtection.Core.Cache;
using MotorProtection.Core.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using MotorProtection.JobManager.Controller;

namespace MotorProtection.JobManager
{
    public partial class MainService : ServiceBase
    {
        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            CacheController.Initialize();

            JobController.Start();

            LogController.LogEvent(AuditingLevel.High, "Service", "Started").Write();
        }

        protected override void OnStop()
        {
            LogController.LogEvent(AuditingLevel.High, "Service", "Stopping").Write();

            JobController.Stop();

            LogController.LogEvent(AuditingLevel.High, "Service", "Stopped").Write();
        }
    }
}
