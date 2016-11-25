#region Using Statements
using System;
using System.Configuration;
#endregion

namespace Examples.Configuration
{
    /// &lt;summary>
    /// An example configuration section class.
    /// &lt;/summary>
    public class ExampleSection : ConfigurationSection
    {
        #region Constructors
        /// &lt;summary>
        /// Predefines the valid properties and prepares
        /// the property collection.
        /// &lt;/summary>
        static ExampleSection()
        {
            // Predefine properties here
            s_propString = new ConfigurationProperty(
                "stringValue",
                typeof(string),
                null,
                ConfigurationPropertyOptions.IsRequired
            );

            s_propBool = new ConfigurationProperty(
                "boolValue",
                typeof(bool),
                false,
                ConfigurationPropertyOptions.None
            );

            s_propTimeSpan = new ConfigurationProperty(
                "timeSpanValue",
                typeof(TimeSpan),
                null,
                ConfigurationPropertyOptions.None
            );

            s_properties = new ConfigurationPropertyCollection();

            s_properties.Add(s_propString);
            s_properties.Add(s_propBool);
            s_properties.Add(s_propTimeSpan);
        }
        #endregion

        #region Static Fields
        private static ConfigurationProperty s_propString;
        private static ConfigurationProperty s_propBool;
        private static ConfigurationProperty s_propTimeSpan;

        private static ConfigurationPropertyCollection s_properties;
        #endregion


        #region Properties
        /// &lt;summary>
        /// Gets the StringValue setting.
        /// &lt;/summary>
        [ConfigurationProperty("stringValue", IsRequired = true)]
        public string StringValue
        {
            get { return (string)base[s_propString]; }
        }

        /// &lt;summary>
        /// Gets the BooleanValue setting.
        /// &lt;/summary>
        [ConfigurationProperty("boolValue")]
        public bool BooleanValue
        {
            get { return (bool)base[s_propBool]; }
        }

        /// &lt;summary>
        /// Gets the TimeSpanValue setting.
        /// &lt;/summary>
        [ConfigurationProperty("timeSpanValue")]
        public TimeSpan TimeSpanValue
        {
            get { return (TimeSpan)base[s_propTimeSpan]; }
        }

        /// &lt;summary>
        /// Override the Properties collection and return our custom one.
        /// &lt;/summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get { return s_properties; }
        }
        #endregion
    }
}