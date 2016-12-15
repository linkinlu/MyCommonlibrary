using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Clone
{
    public class MemoryDeepClone
    {
        public static T DeepClone<T>(T obj)
        {
            if (obj == null)
            {
                return default(T);
            }
            if (!obj.GetType().IsSerializable)
            {
                throw new ArgumentException("对象要标记Serializable Att");
            }

            var formatter = new BinaryFormatter();
            using (MemoryStream sm = new MemoryStream())
            {
                formatter.Serialize(sm, obj);
                sm.Seek(0, SeekOrigin.Begin);
                return (T) formatter.Deserialize(sm);
            }
        }

    }
}
