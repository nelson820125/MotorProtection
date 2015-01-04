using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration.Provider;

namespace MotorProtection.Core.Log
{
    public abstract class LogProvider : ProviderBase
    {
        public abstract DateTime Add(string level, int? userId, Dictionary<string, string> attributes);

        protected string Serialize(string level, int? userId, Dictionary<string, string> attributes)
        {
            string log = string.Format("[Level]{0} [UserID]{1}\n", level, userId);
            if (attributes != null && attributes.Count() > 0)
                log += attributes.Select(attr => "[" + attr.Key + "]" + (attr.Value == null ? string.Empty : attr.Value)).Aggregate((current, next) => current + "\n" + next) + "\n";

            return log;
        }
    }
}
