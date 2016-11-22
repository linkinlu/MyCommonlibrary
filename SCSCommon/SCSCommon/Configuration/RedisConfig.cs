using System.Configuration;

namespace SCSCommon.Configuration
{
    public class RedisConfig:ConfigurationElement
    {
        [ConfigurationProperty("RedisConnectionString", IsRequired = true,IsKey = true)]
        public string RedisConnectionString
        {
            get { return this["RedisConnectionString"].ToString(); }
            set { this["RedisConnectionString"] = value; }
        }
    }

    [ConfigurationCollection(typeof(RedisConfig))]
    public class RedisConfigCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RedisConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RedisConfig) element).RedisConnectionString;
        }
    }

    public class RedisConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("RedisSection")]
        public RedisConfigCollection RedisSection
        {

            get { return (RedisConfigCollection) base["RedisSection"]; }
        }
    }
}