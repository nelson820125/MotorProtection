using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;
using MotorProtection.Core.Common;
using System.Web.Configuration;
using System.Collections.Specialized;
using MotorProtection.Core.Data.Entities;

namespace MotorProtection.Core.Log
{
    public static class LogController
    {
        private static LogProvider s_loggingProvider;
        private static int s_loggingLevel;

        private static LogProvider s_auditingProvider;
        private static int s_auditingLevel;

        static LogController()
        {
            InitLoggingProvider();
            InitAuditingProvider();
        }

        private static void InitLoggingProvider()
        {
            ProviderSection config = (ProviderSection)ConfigurationManager.GetSection("alpha/logging");

            ProviderCollection providers = new ProviderCollection();

            // use the ProvidersHelper class to call Initialize() on each provider
            ProvidersHelper.InstantiateProviders(config.Providers, providers, typeof(LogProvider));

            s_loggingProvider = (LogProvider)providers[config.DefaultProvider];

            NameValueCollection settings = config.Providers[config.DefaultProvider].Parameters;
            s_loggingLevel = int.Parse(settings["level"]);
        }

        private static void InitAuditingProvider()
        {
            ProviderSection config = (ProviderSection)ConfigurationManager.GetSection("alpha/auditing");

            ProviderCollection providers = new ProviderCollection();

            // use the ProvidersHelper class to call Initialize() on each provider
            ProvidersHelper.InstantiateProviders(config.Providers, providers, typeof(LogProvider));

            s_auditingProvider = (LogProvider)providers[config.DefaultProvider];

            NameValueCollection settings = config.Providers[config.DefaultProvider].Parameters;
            s_auditingLevel = int.Parse(settings["level"]);
        }

        public static LogInfo LogError(int level, Exception e = null)
        {
            LogInfo logInfo = new LogInfo(s_loggingProvider);

            if (s_loggingLevel == -1 || (s_loggingLevel != 0 && s_loggingLevel >= level))
            {
                logInfo.Enabled = true;

                InitLogInfo(logInfo, level);
            }
            else logInfo.Enabled = false;

            return logInfo;
        }

        public static LogInfo LogError(LoggingLevel level, Exception e = null)
        {
            return LogError((int)level, e);
        }

        public static LogInfo LogEvent(AuditingLevel level, string target = null, string action = null)
        {
            return LogEvent((int)level, target, action);
        }

        public static LogInfo LogEvent(int level, string type, string action)
        {
            LogInfo logInfo = new LogInfo(s_auditingProvider);

            if (s_auditingLevel == -1 || (s_auditingLevel != 0 && s_auditingLevel >= level))
            {
                logInfo.Enabled = true;

                InitLogInfo(logInfo, level);

                if (!string.IsNullOrEmpty(type)) logInfo.Add("Type", type);
                if (!string.IsNullOrEmpty(action)) logInfo.Add("Action", action);
            }
            else logInfo.Enabled = false;

            return logInfo;
        }

        private static void InitLogInfo(LogInfo logInfo, int level)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var user = ctt.Users.FirstOrDefault(u => u.IsLocked == false && u.Enabled == true);

                logInfo.UserId = user.UserID;
                logInfo.ImpersonateBy = user.UserID;
            }

            logInfo.Level = level;
        }
    }
}
