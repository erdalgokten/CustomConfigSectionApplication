using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomConfigSectionApplication
{
    public sealed class Config
    {
        private static volatile Config instance;
        private static object syncRoot = new Object();
        public MyElements MyElements { get; private set; }

        private Config()
        {
            this.MyElements = ((MySection)ConfigurationManager.GetSection("mySectionGroup/mySection")).MyElements;
        }

        public static Config Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Config();
                    }
                }

                return instance;
            }
        }
    }

    public class MySection : ConfigurationSection
    {
        [ConfigurationProperty("myElements", IsRequired = true)]
        public MyElements MyElements
        {
            get
            {
                return base["myElements"] as MyElements;
            }
        }
    }

    [ConfigurationCollection(typeof(MyElement), AddItemName = "myElement")]
    public class MyElements : ConfigurationElementCollection, IEnumerable<MyElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var configElement = element as MyElement;
            if (configElement != null)
                return configElement.Name;
            else
                return null;
        }

        public MyElement this[int index]
        {
            get
            {
                return BaseGet(index) as MyElement;
            }
        }

        public MyElement GetConfigElement(string key)
        {
            return BaseGet(key) as MyElement;
        }

        IEnumerator<MyElement> IEnumerable<MyElement>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }

    public class MyElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return base["name"] as string; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("enabled", DefaultValue = "false", IsRequired = false)]
        public Boolean Enabled
        {
            get { return (Boolean)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        [ConfigurationProperty("myFrequencies")]
        public MyFrequencyCollection MyFrequencies
        {
            get { return base["myFrequencies"] as MyFrequencyCollection; }
        }

        [ConfigurationProperty("mySpecifics")]
        public MySpecifics MySpecifics
        {
            get { return (MySpecifics)this["mySpecifics"]; }
            set { this["mySpecifics"] = value; }
        }
    }

    [ConfigurationCollection(typeof(MyFrequency), AddItemName = "myFrequency")]
    public class MyFrequencyCollection : ConfigurationElementCollection, IEnumerable<MyFrequency>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MyFrequency();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var configElement = element as MyFrequency;
            if (configElement != null)
                return configElement.Name;
            else
                return null;
        }

        public MyFrequency this[int index]
        {
            get
            {
                return BaseGet(index) as MyFrequency;
            }
        }

        IEnumerator<MyFrequency> IEnumerable<MyFrequency>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }

    public class MyFrequency : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return base["name"] as string; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("start", DefaultValue = "09:00:00", IsRequired = true)]
        public TimeSpan Start
        {
            get { return (TimeSpan)this["start"]; }
            set { this["start"] = value; }
        }

        [ConfigurationProperty("end", DefaultValue = "19:00:00", IsRequired = true)]
        public TimeSpan End
        {
            get { return (TimeSpan)this["end"]; }
            set { this["end"] = value; }
        }

        [ConfigurationProperty("checkInEveryXMinutes", DefaultValue = "10", IsRequired = true)]
        public int CheckInEveryXMinutes
        {
            get { return (int)this["checkInEveryXMinutes"]; }
            set { this["checkInEveryXMinutes"] = value; }
        }
    }

    public class MySpecifics : ConfigurationElement
    {
        [ConfigurationProperty("checkLastItem", DefaultValue = "true", IsRequired = false)]
        public Boolean CheckLastItem
        {
            get { return (Boolean)this["checkLastItem"]; }
            set { this["checkLastItem"] = value; }
        }

        [ConfigurationProperty("bulkCount", DefaultValue = "10", IsRequired = true)]
        public int BulkCount
        {
            get { return (int)this["bulkCount"]; }
            set { this["bulkCount"] = value; }
        }
    }
}
