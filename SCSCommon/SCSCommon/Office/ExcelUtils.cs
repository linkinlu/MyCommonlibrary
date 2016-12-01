using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCSCommon.Directory;
using System.IO;
using System.Reflection;
using NPOI.SS.UserModel;
using SCSCommon.Strings;
using SCSCommon.DateTimeExt;
using SCSCommon.Emunerable;

namespace SCSCommon.Office
{
    public class ExcelUtils
    {
        public const string extension2003 = ".xls";
        public const string extension2007 = ".xls";
        public void ReadExcel2003(string filePath)
        {
            var book = new HSSFWorkbook();
        }

        /// <summary>
        /// Exports to excel2003.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basePath">The base path.</param>
        /// <param name="entity">The entity.通常是业务数据 entity 或者 DTO</param>
        /// <param name="fileName">Name of the file.</param>
        public static void ExportToExcel2003<T>(string basePath, List<T> entity, string fileName = "")
        {
            if (entity == null) return;
            if (!basePath.IsVaildFilePath()) return;


            var fullName = Path.Combine(basePath,
                string.IsNullOrEmpty(fileName) ? DateTime.Now.ToyyyyMMddHHmmss() + extension2003 : fileName + extension2003);

            try
            {

                var book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet();
                var objType = typeof(T);
                if (objType.IsClass)
                {
                    var first = sheet.CreateRow(0);
                    var valueRow = sheet.CreateRow(1);
                    var includeProperty =  objType.GetProperties(BindingFlags.GetProperty| BindingFlags.Public).ToList();

                    includeProperty.EachWithIndex((index, prop) =>
                    {
                        var colCell = first.CreateCell(index);
                        colCell.SetCellValue(prop.Name);

                        var valueCell = valueRow.CreateCell(index);
                        valueCell.SetCellValue(prop.GetValue(null) == null ? string.Empty: prop.GetValue(null).ToString());
                    });
                }


                using (FileStream sm = new FileStream(fullName, FileMode.OpenOrCreate))
                {
                    book.Write(sm);
                }
            }
            catch (Exception ex)
            {
                
               
            }

        }


        public static void ExportToExcel2007<T>(string basePath,T entity)
        {


        }
    }
}
