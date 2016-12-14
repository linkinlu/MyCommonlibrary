using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel;
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

        public static void ReadXls<T>(Stream stream, IList<PropertyByName<T>> properties,
            bool is2003 = true)
        {

            IExcelDataReader excelReader = is2003
                ? ExcelReaderFactory.CreateBinaryReader(stream)
                : ExcelReaderFactory.CreateOpenXmlReader(stream);

            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();

            excelReader.Close();

            if (result != null && result.Tables.Count > 0)
            {
              

            }

        }
    }
}
