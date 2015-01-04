using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MotorProtection.Core.Common
{
    public class CacheSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public CacheGroupElementCollection CacheGroupElements
        {
            get { return (CacheGroupElementCollection)base[""]; }
        }
    }

    public class CacheGroupElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheGroupElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CacheGroupElement)element).Group;
        }

        public CacheGroupElement this[int index]
        {
            get { return (CacheGroupElement)BaseGet(index); }
        }

        public new CacheGroupElement this[string group]
        {
            get { return (CacheGroupElement)BaseGet(group); }
        }
    }

    public class CacheGroupElement : ConfigurationElement
    {
        [ConfigurationProperty("group", IsRequired = true, IsKey = true)]
        public string Group
        {
            get { return (string)base["group"]; }
            set { base["group"] = value; }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public CacheElementCollection CacheElements
        {
            get { return (CacheElementCollection)base[""]; }
        }
    }

    public class CacheElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CacheElement)element).Name;
        }

        public CacheElement this[int index]
        {
            get { return (CacheElement)BaseGet(index); }
        }

        public new CacheElement this[string name]
        {
            get { return (CacheElement)BaseGet(name); }
        }
    }

    public class CacheElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("required", DefaultValue = "true")]
        public bool Required
        {
            get { return (bool)base["required"]; }
            set { base["required"] = value; }
        }

        /// <summary>
        /// number of seconds the cache expires, 0: never expires.
        /// NOTE: the cache will be updated anyway when its cacheGroup has a new timestamp
        /// </summary>
        [ConfigurationProperty("expires", DefaultValue = "0")]
        public int Expires
        {
            get { return (int)base["expires"]; }
            set { base["expires"] = value; }
        }
    }
}
