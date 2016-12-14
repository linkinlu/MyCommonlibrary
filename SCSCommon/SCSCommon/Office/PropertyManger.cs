using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace SCSCommon.Office
{
    public class PropertyManger<T>
    {
        private IList<PropertyByName<T>> pro;
        private Dictionary<string, int> dic = new Dictionary<string, int>();
        public T CurrentObj { get; set; }

        public PropertyManger(IList<PropertyByName<T>> property)
        {
            pro = property;
        }

        public void SetHeader(ISheet sheet,int offset  = 0 )
        {
            IRow first = sheet.CreateRow(0);
            for (int i = 0; i < pro.Count(); i++)
            {
                ICell cell = first.CreateCell(i + offset);
                cell.SetCellValue(pro[i].Name);
            }
        }

        public PropertyByName<T> GetProperty(string name)
        {
            return pro.FirstOrDefault(t => t.Name == name);
        }

        public void WriteCell(ISheet sheet, int rowIndex, int offset = 0)
        {
            IRow first = sheet.CreateRow(rowIndex);
            for (int i = 0; i < pro.Count(); i++)
            {
                ICell cell = first.CreateCell(i + offset);
                var actualVal = pro[i].Func(CurrentObj);
                cell.SetCellValue(actualVal == null ? string.Empty : actualVal.ToString());
            }

        }

        internal void ReadHeader(ISheet workSheet, int offset = 0)
        {
            IRow first = workSheet.GetRow(0);
            for (int i = 0; i < first.Cells.Count(); i++)
            {
                
                var found =
                    pro.FirstOrDefault(
                        t => t.Name.Equals(first.Cells[i].StringCellValue, StringComparison.OrdinalIgnoreCase));
                if (found != null)
                {
                    found.ColumnOrder = i;
                }
            }
        }


        public void ReadContent(ISheet workSheet,int rowIndex)
        {
            IRow row = workSheet.GetRow(rowIndex);

            var hasColumOrder = pro.Where(t => t.ColumnOrder > -1);

            if (hasColumOrder != null)
            {
                foreach (var col in hasColumOrder)
                {
                    col.PropertyVal = row.Cells[col.ColumnOrder].StringCellValue;
                }
            }
        }
    }
}
