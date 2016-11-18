using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Convertion
{
    public static class BasicConvert
    {

        //refer from James Craig utilities library
        public static R To<T, R>(this T Object, R DefaultValue = default(R))
        {
            return (R) To(Object, typeof(R), DefaultValue);
        }

        private static object To<T>(T Item, Type ResultType, object DefaultValue = null)
        {
            try
            {
                if (Item == null)
                {
                    return (DefaultValue == null && ResultType.IsValueType) ?
                        Activator.CreateInstance(ResultType) :
                        DefaultValue;
                }
                var ObjectType = Item.GetType();
                if (ObjectType == typeof(DBNull))
                {
                    return (DefaultValue == null && ResultType.IsValueType) ?
                        Activator.CreateInstance(ResultType) :
                        DefaultValue;
                }
                if (ResultType.IsAssignableFrom(ObjectType))
                    return Item;
                if (Item as IConvertible != null && !ObjectType.IsEnum && !ResultType.IsEnum)
                    return Convert.ChangeType(Item, ResultType, CultureInfo.InvariantCulture);
                var Converter = TypeDescriptor.GetConverter(Item);
                if (Converter.CanConvertTo(ResultType))
                    return Converter.ConvertTo(Item, ResultType);
                Converter = TypeDescriptor.GetConverter(ResultType);
                if (Converter.CanConvertFrom(ObjectType))
                    return Converter.ConvertFrom(Item);
                if (ResultType.IsEnum)
                {
                    if (ObjectType == ResultType.GetEnumUnderlyingType())
                        return System.Enum.ToObject(ResultType, Item);
                    if (ObjectType == typeof(string))
                        return System.Enum.Parse(ResultType, Item as string, true);
                }
                // 类的比对 TODO:Should automap and copy value to destinty
                if (ResultType.IsClass)
                {
                    //var ReturnValue = Activator.CreateInstance(ResultType);
                    //var TempMapping = ObjectType.MapTo(ResultType);
                    //if (TempMapping == null)
                    //    return ReturnValue;
                    //TempMapping
                    //    .AutoMap()
                    //    .Copy(Item, ReturnValue);
                    return null;
                }
            }
            catch
            {
            }
            return (DefaultValue == null && ResultType.IsValueType) ?
                Activator.CreateInstance(ResultType) :
                DefaultValue;
        }

    }
}
