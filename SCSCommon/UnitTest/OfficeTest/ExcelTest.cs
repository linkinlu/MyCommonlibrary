using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSCommon.DataTableEX;
using SCSCommon.Office;
using System.Data;

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
                },
                 new Company()
                {
                    ID = 4,
                    Name = "asdas",
                    CreateTime = DateTime.Now,
                    Department = new Department() {ID = 1, Name = "12@asd"},
                    IsLocked = false,
                    Reason = CloseReason.ExceptionError
                }
            };


            //ExcelUtils.ExportToExcel2003("D:\\", ls);
            //var items1 = ExcelUtils.ReadExcel2003<Company>("D:\\20161202140848.xls");


            var nsm = new List<PropertyByName<Company>>()
            {
                new PropertyByName<Company>("ID", t => t.ID),
                new PropertyByName<Company>("name", t => t.Name, false),
                new PropertyByName<Company>("departmentname", t => t.Department.Name),
                new PropertyByName<Company>("IsLocked", t => t.IsLocked)
            };

            var bbb = ExportManger.ExportExcel<Company>(nsm, ls);
            using (FileStream fm = new FileStream("D:\\20161202140848.xls", FileMode.CreateNew))
            {
                fm.Write(bbb, 0, bbb.Count());
            }



            var manager = new PropertyManger<Company>(new List<PropertyByName<Company>>()
            {
                new PropertyByName<Company>("ID"),
                new PropertyByName<Company>("name"),
                new PropertyByName<Company>("departmentname")
            });
            using (FileStream fm = new FileStream("D:\\20161202140848.xls", FileMode.Open))
            {
                var dt = ImportManager.ReadXls<Company>(fm);
                var got = from a in dt.AsEnumerable()
                    select new
                    {
                        ID = a.Field<string>("ID"),
                        DepartmentName = a.Field<string>("departmentname"),
                        name = a.Field<string>("name"),
                    };

                Assert.IsNotNull(got);

            }

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

