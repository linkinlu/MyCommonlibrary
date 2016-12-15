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
    /// 使用第三方exceldatareader类库 有linqtoexcel 有时候研究下!
    /// </summary>
    public class ImportManager
    {

        public static DataTable ReadXls<T>(Stream stream, bool is2003 = true)
        {

            IExcelDataReader excelReader = is2003
                ? ExcelReaderFactory.CreateBinaryReader(stream)
                : ExcelReaderFactory.CreateOpenXmlReader(stream);

            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();

            excelReader.Close();

            if (result != null && result.Tables.Count > 0)
            {
                return result.Tables[0];

            }
            return null;
        }
    }
}
