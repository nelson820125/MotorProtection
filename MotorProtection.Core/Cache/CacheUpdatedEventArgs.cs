using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Cache
{
    public class CacheUpdatedEventArgs : EventArgs
    {
        private string _key;
        private bool _initialize;

        public CacheUpdatedEventArgs(string key, bool initialize)
        {
            this._key = key;
            this._initialize = initialize;
        }

        public string Key
        {
            get { return _key; }
        }

        /// <summary>
        /// true: fired in Initialize
        /// false: fired in Synchronize
        /// </summary>
        public bool Initialize
        {
            get { return _initialize; }
        }
    }
}
