using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSCommon.DataTableEX;
using SCSCommon.Office;

namespace UnitTest.OfficeTest
{
    [TestFixture]

    class ExcelTest
    {

        [Test]
        public void test()
        {
            var ls = new List<Company>()
            {
                new Company()
                {
                    ID = 3,
                    Name = "123",
                    CreateTime = DateTime.Now,
                    Department = new Department() {ID = 1, Name = "departmenta"},
                    IsLocked = true,
                    Reason = CloseReason.ExceptionError
                }
            };

            var items = ls.ToDataTable();

            ExcelUtils.ExportToExcel2003("D:\\", ls);
            var items1 = ExcelUtils.ReadExcel2003<Company>("D:\\20161202140848.xls");


        }
    }
    
    public class Company
    {
  
        public int ID { get; set; }

        
        public string Name { get; set; }


        public DateTime CreateTime { get; set; }

        public bool IsLocked { get; set; }

        public Department Department { get; set; }

        public CloseReason Reason { get; set; }
    }

    public class Department
    {
        public string Name { get; set; }

        public int ID { get; set; }
    }


    public enum CloseReason
    {
        UserClose,
        ExceptionError
    }

}
