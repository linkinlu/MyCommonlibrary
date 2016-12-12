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
using SCSCommon.DateTimeExt;
using SCSCommon.Emunerable;

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


            try
            {

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
            catch (Exception ex)
            {

                return null;
            }
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
                string.IsNullOrEmpty(fileName) ? DateTime.Now.ToyyyyMMddHHmmss() + extension2003 : fileName + extension2003);



            try
            {

                var book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet();

                #region 创建excel的summary信息
                //var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                //dsi.Company = "NPOI";
                //book.DocumentSummaryInformation = dsi;
                //SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                //si.Author = "文件作者信息";
                //si.ApplicationName = "创建程序信息";
                //si.LastAuthor = "最后保存者信息";
                //si.Comments = "作者信息";
                //si.Title = "标题信息";
                //si.Subject = "主题信息";
                //si.CreateDateTime = DateTime.Now;
                //book.SummaryInformation = si;
                #endregion


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
            catch (Exception ex)
            {


            }
        }

        public static void ExportToExcel2007<T>(string basePath, List<T> entity, string fileName = "")
        {

            if (entity == null) return;
            if (!basePath.IsVaildFilePath()) return;


            var fullName = Path.Combine(basePath,
                string.IsNullOrEmpty(fileName) ? DateTime.Now.ToyyyyMMddHHmmss() + extension2007: fileName + extension2007);



            try
            {

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
            catch (Exception ex)
            {


            }
        }


    }
}
