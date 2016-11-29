using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCSCommon.Directory;
using System.IO;

namespace SCSCommon.Office
{
    public class ExcelUtils
    {

        public void ReadExcel2003(string filePath)
        {
            var book = new HSSFWorkbook();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basePath"></param>
        /// <param name="entity"></param>
        public static void ExportToExcel2003<T>(string basePath,T entity,string fileName = null)
        {
           

            CommonDirectoryHelper.MapPath(basePath);

            if (string.IsNullOrEmpty(fileName))
            {
                //fileName = DateTime.Now.ToString("yyyyMM");
            }



        }


        public static void ExportToExcel2007<T>(string basePath,T entity)
        {


        }
    }
}
