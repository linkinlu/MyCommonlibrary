using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace SCSCommon.Directory
{
    public class CommonDirectoryHelper
    {
        /// <summary>
        /// 通用于web及app的路径搜索,只适应于在路径 不适合文件 !!
        /// </summary>
        /// <param name="path">格式"~/*****"</param> 
        /// <returns></returns>
        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }

            ///TRIMStart 的用法从字符串头去除包含这个关键字及之前的字符   如该字符串第一个字符不等于关键字就 直接返回这个字符串 
            ///有点忘记了 ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！ 温故知新
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                path.Replace("~/", "").TrimStart('/').Replace("/", "\\"));
        }

        /// <summary>
        /// Maps the full name of the file. 
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string MapFullFileName(string path, string fileName)
        {
            if (HostingEnvironment.IsHosted)
            {
                return Path.Combine(HostingEnvironment.MapPath(path), fileName);
            }

            return Path.Combine(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    path.Replace("~/", "").TrimStart('/').Replace("/", "\\")), fileName);
        }

    }
}
