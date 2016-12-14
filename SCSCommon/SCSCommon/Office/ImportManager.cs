using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace SCSCommon.Office
{
    /// <summary>
    /// 尝试了很多种方式，但还没找到最优的!!!!!!!!!目前这种做法是错误的
    /// </summary>
    public class ImportManager
    {
        private IList<PropertyByName<object>> properties;



        public static List<IList<PropertyByName<T>>> ReadXls<T>(Stream stream, IList<PropertyByName<T>> properties, bool is2003 = true)
        {
            using (MemoryStream sm = new MemoryStream())
            {
                try
                {

                    var book = is2003 ? new HSSFWorkbook(stream) : new XSSFWorkbook(stream) as IWorkbook;
                    var workSheet = book.GetSheetAt(0);

                    var manager = new PropertyManger<T>(properties);

                    manager.ReadHeader(workSheet);

                    var offset = 1;
                    var rowIncrease = offset;
                    var lastRowNum = workSheet.LastRowNum;
                    var dic = new List<IList<PropertyByName<T>>>();
                    for (int i = offset; i < lastRowNum; i++)
                    {

                        manager.ReadContent(workSheet, rowIncrease);
                        dic.Add(properties);
                        rowIncrease++;
                    }
                    return dic;
                }
                catch
                {
                    return null;
                }
            }


        }

        //public static void ReadXls<T>(Stream stream, PropertyManger<T> manager, bool is2003 = true)
        //{
        //    using (MemoryStream sm = new MemoryStream())
        //    {
        //        try
        //        {

        //            var book = is2003 ? new HSSFWorkbook(stream) : new XSSFWorkbook(stream) as IWorkbook;
        //            var workSheet = book.GetSheetAt(0);


        //            manager.ReadHeader(workSheet);

        //            var offset = 1;
        //            var rowIncrease = offset;
        //            var lastRowNum = workSheet.LastRowNum;
        //            var managers = new List<PropertyManger<T>>();
        //            for (int i = offset; i < lastRowNum; i++)
        //            {

        //                manager.ReadContent(workSheet, rowIncrease);
        //                managers.Add(manager);
        //                rowIncrease++;
        //            }

        //            //return managers;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}
