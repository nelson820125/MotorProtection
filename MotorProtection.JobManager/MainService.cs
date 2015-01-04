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

            LogController.LogEvent(AuditingLevel.Level1, "Service", "Started").Write();
        }

        protected override void OnStop()
        {
            LogController.LogEvent(AuditingLevel.Level1, "Service", "Stopping").Write();

            JobController.Stop();

            LogController.LogEvent(AuditingLevel.Level1, "Service", "Stopped").Write();
        }
    }
}
