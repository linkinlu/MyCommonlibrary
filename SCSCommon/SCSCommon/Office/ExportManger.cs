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
    public class ExportManger
    {
        /// <summary>
        /// Exports the excel.所有字段都是string类型的读取时需要自己转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The property.</param>
        /// <param name="entities">The entities.</param>
        /// <param name="is2003">if set to <c>true</c> [is2003].</param>
        /// <returns></returns>
        public static byte[] ExportExcel<T>(IList<PropertyByName<T>> property, IList<T> entities,bool is2003 = true)
        {
            if (property == null || !property.Any() || entities == null || !entities.Any())
            {
                return null;
            }


            using (MemoryStream sm = new MemoryStream())
            {
                var book = is2003 ? new HSSFWorkbook() : new XSSFWorkbook() as IWorkbook;

                var workSheet = book.CreateSheet();

                var manager = new PropertyManger<T>(property.Where(t => !t.Ingore).ToList());
                manager.SetHeader(workSheet);


                int secondRow = 1;
                for (int i = 0; i < entities.Count(); i++)
                {
                    manager.CurrentObj = entities[i];
                    manager.WriteCell(workSheet, secondRow);
                    secondRow++;
                }

                book.Write(sm);
                return sm.ToArray();
            }
        }


        

    }
}
