using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Log
{
    public class LogInfo
    {
        public string Level { get; set; }
        public int? UserId { get; set; }
        public int? ImpersonateBy { get; set; }
        private Dictionary<string, string> _attributes = new Dictionary<string, string>();

        public bool Enabled { get; set; }

        private LogProvider _provider;

        public LogInfo(LogProvider provider)
        {
            this._provider = provider;
        }

        public LogInfo Add(string name, object value)
        {
            if (!Enabled) return this;

            AddAttribute(name, value == null ? "" : value.ToString());
            return this;
        }

        private void AddAttribute(string name, string value)
        {
            var key = name;
            int i = 0;
            while (this._attributes.ContainsKey(key))
            {
                i++;
                key = name + "_" + i.ToString();
            }
            this._attributes.Add(key, value);
        }

        public DateTime Write()
        {
            if (!Enabled) return DateTime.Now;

            if (ImpersonateBy != null) AddAttribute("ImpersonateBy", ImpersonateBy.Value.ToString());

            return this._provider.Add(Level, UserId, this._attributes);
        }
    }
}
