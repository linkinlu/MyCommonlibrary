using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCSCommon.Directory;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SCSCommon.Convertion;
using SCSCommon.Extension;

using SCSCommon.Emunerable;
using System.IO;

namespace SCSCommon.Office
{
    public class ExcelUtils
    {
        public const string extension2003 = ".xls";
        public const string extension2007 = ".xlsx";

        public static List<T> ReadExcel2003<T>(string filePath,int headerRowindex = 0)
        {
            if (!File.Exists(filePath)) return null;
            List<T> lst = new List<T>();

                using (FileStream  fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var book = new HSSFWorkbook(fs);
                    ISheet sheet = book.GetSheetAt(0);
                    if (sheet == null) return null;

                    IRow headerRow = sheet.GetRow(headerRowindex);
                    IRow valueRow = sheet.GetRow(headerRowindex + 1);

                    var totalCount = sheet.LastRowNum - headerRowindex ;

                    for (int i = 0; i < totalCount; i++)
                    {
                        T created = Activator.CreateInstance<T>();
                        valueRow.EachWithIndex((index, cellIndex) =>
                        {
                            var property =typeof(T).GetProperties().FirstOrDefault(t =>t.Name.Equals(headerRow.GetCell(index).StringCellValue,StringComparison.OrdinalIgnoreCase));

                            if (property != null)
                            {
                                //如何转化为正确的类型!!!!!!!!!!!!!!!!!!!!TODO
                                var propValue = Activator.CreateInstance(property.GetType());
                                var actualValue = BasicConvert.To(valueRow.GetCell(index).StringCellValue, propValue);
                                property.SetValue(created, actualValue);
                            }
                        });
                    }


                }
                return lst;
            }
    

        /// <summary>
        /// Exports to excel2003.   
        /// 目前只支持简单数据类型
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
                string.IsNullOrEmpty(fileName) ? DateTime.Now.ToDateTimeString() + extension2003 : fileName + extension2003);

                var book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet();

                #region 导出

                var objType = typeof(T);
                if (objType.IsClass)
                {
                    var first = sheet.CreateRow(0);
                    var includeProperty =
                        objType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                            .ToList()
                            .OrderBy(t => t.Name)
                            .ToList();

                    //      PropertyDescriptorCollection properties =
                    //TypeDescriptor.GetProperties(typeof(T));

                    includeProperty.EachWithIndex((index, prop) =>
                    {
                        var colCell = first.CreateCell(index);
                        colCell.SetCellValue(prop.Name);
                    });
                    var firstRow = 0;
                    var ser = new JavaScriptSerializer();
                    entity.EachWithIndex((rowIndex, prop) =>
                    {
                        firstRow++;
                        var valueRow = sheet.CreateRow(firstRow);
                        includeProperty.EachWithIndex((cellindex, property) =>
                        {
                            var colCell = valueRow.CreateCell(cellindex);
                            var propertyValue = property.GetValue(prop);
                            if (property.PropertyType.IsPrimitive || property.PropertyType.IsValueType)
                            {
                                colCell.SetCellValue(propertyValue == null ? string.Empty : propertyValue.ToString());
                            }
                            else

                            {
                                colCell.SetCellValue(propertyValue == null
                                    ? string.Empty
                                    : ser.Serialize(propertyValue));
                            }
                        });
                    });

                }
                #endregion

                using (FileStream sm = new FileStream(fullName, FileMode.OpenOrCreate))
                {
                    book.Write(sm);
                }
            
        
        }

        public static void ExportToExcel2007<T>(string basePath, List<T> entity, string fileName = "")
        {

            if (entity == null || entity.Count == 0) return;
            if (!basePath.IsVaildFilePath()) return;

            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }

            var fullName = Path.Combine(basePath,
                string.IsNullOrEmpty(fileName) ? DateTime.Now.ToChineseDateTimeString() + extension2007: fileName + extension2007);



      
                var book = new XSSFWorkbook();
                ISheet sheet = book.CreateSheet();

               


                #region 导出

                var objType = typeof(T);
                if (objType.IsClass)
                {
                    var first = sheet.CreateRow(0);
                    var includeProperty =
                        objType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                            .ToList()
                            .OrderBy(t => t.Name)
                            .ToList();

                    //      PropertyDescriptorCollection properties =
                    //TypeDescriptor.GetProperties(typeof(T));

                    includeProperty.EachWithIndex((index, prop) =>
                    {
                        var colCell = first.CreateCell(index);
                        colCell.SetCellValue(prop.Name);
                    });
                    var firstRow = 0;
                    var ser = new JavaScriptSerializer();
                    entity.EachWithIndex((rowIndex, prop) =>
                    {
                        firstRow++;
                        var valueRow = sheet.CreateRow(firstRow);
                        includeProperty.EachWithIndex((cellindex, property) =>
                        {
                            var colCell = valueRow.CreateCell(cellindex);
                            var propertyValue = property.GetValue(prop);
                            if (property.PropertyType.IsPrimitive || property.PropertyType.IsValueType)
                            {
                                colCell.SetCellValue(propertyValue == null ? string.Empty : propertyValue.ToString());
                            }
                            else

                            {
                                colCell.SetCellValue(propertyValue == null
                                    ? string.Empty
                                    : ser.Serialize(propertyValue));
                            }
                        });
                    });

                }
                #endregion

                using (FileStream sm = new FileStream(fullName, FileMode.OpenOrCreate))
                {
                    book.Write(sm);
                }
            }
       

    }
}
