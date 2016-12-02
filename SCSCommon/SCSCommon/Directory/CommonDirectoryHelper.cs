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
        /// 通用于web及app的路径搜索,只适应于在appfolder
        /// </summary>
        /// <param name="path">格式"~/*****"</param> 
        /// <returns></returns>
        public static string MapPath(string path,string basePath = "")
        {
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }

            ///TRIMStart 的用法从字符串头去除包含这个关键字及之前的字符   如该字符串第一个字符不等于关键字就 直接返回这个字符串 
            ///有点忘记了 ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！ 温故知新
            return Path.Combine(string.IsNullOrEmpty(basePath) ? AppDomain.CurrentDomain.BaseDirectory : basePath,
                path.Replace("~/", "\\").TrimStart('/').Replace("/", "\\"));
        }


        

    }
}
