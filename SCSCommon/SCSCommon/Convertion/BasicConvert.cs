using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Convertion
{
    public class BasicConvert
    {
        public static T To<T>(object value)
        {
            return (T)To(value, typeof(T));
        }

        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        private static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsInstanceOfType(value))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }
    }
}
