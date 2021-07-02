using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCSCommon.Emunerable;

namespace SCSCommon.Extension
{
    public static class ArrayEx
    {
        public static bool HasData<T>(this T[] t)
        {
            return t != null && t.Any();
        }


        public static async  Task ForEachAsync<T>(this T[] data, Func<T,Task> func)
        {
            if (data.HasValue())
            {
                for (int i = 0; i < data.Length; i++)
                {
                    await func(data[i]);
                }
            }
        }
    }
}
