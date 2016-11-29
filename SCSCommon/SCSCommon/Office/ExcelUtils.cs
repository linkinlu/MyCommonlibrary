using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCSCommon.Directory;
using System.IO;
using NPOI.SS.UserModel;
using SCSCommon.Strings;
using SCSCommon.DateTimeExt;

namespace SCSCommon.Office
{
    public class ExcelUtils
    {

        public void ReadExcel2003(string filePath)
        {
            var book = new HSSFWorkbook();
        }

        /// <summary>
        /// Exports to excel2003.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basePath">The base path.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void ExportToExcel2003<T>(string basePath, T entity, string fileName = "")
        {
            if (entity == null) return;
            if (!basePath.IsVaildFilePath()) return;


            var fullName = Path.Combine(basePath,
                string.IsNullOrEmpty(fileName) ? DateTime.Now.ToyyyyMMddHHmmss() : fileName + ".xls");

            try
            {
                var objType = typeof(T);
                 //if(objType.IsClass &&)  
                var book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet();
               






            }
            catch (Exception)
            {
                
               
            }

        }


        public static void ExportToExcel2007<T>(string basePath,T entity)
        {


        }
    }
}
