using System;
using System.Collections.Generic;
using System.Linq;


namespace SCSCommon.Emunerable
{
    public static class EnumerableUtils
    {
        public static void Each<T>(this IEnumerable<T> col, Action<T> action)
        {
            if (col != null && col.Any())
            {
                foreach (T cur in col)
                {
                    action(cur);
                }
            }
        }

        /// <summary>
        /// 只要有一个元素不符合要求就 返回false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static bool Each<T>(this IEnumerable<T> col, Func<T,bool> func)
        {
            if (col == null) return false;
            if (!col.Any()) return false;

            var result = false;
            if (col != null && col.Any())
            {
                foreach (T cur in col)
                {
                    result =  func(cur);
                    if (!result)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool EachWithIndex<T>(this IEnumerable<T> col, Func<int, T, bool> func)
        {
            if (col == null) return false;
            if (!col.Any()) return false;


            var result = false;
            if (col != null && col.Any())
            {

                var lst = col.ToList();
                for (int i = 0; i < lst.Count(); i++)
                {
                    result = func(i, lst[i]);
                    if (!result)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void EachWithIndex<T>(this IEnumerable<T> col, Action<int,T> action)
        {
            if (col != null && col.Any())
            {
                
                var lst = col.ToList();
                for (int i = 0; i < lst.Count(); i++)
                {
                    action(i, lst[i]);
                }
            }
        }

        public static bool HasValue<T>(this IEnumerable<T> col)
        {
            return col != null && col.Any();
        }
    }
}
