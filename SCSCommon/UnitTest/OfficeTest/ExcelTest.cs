using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
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
                    Code = "AI",
                    MaxNumberOfUser = 525,
                    IsDeleted = false,
                    IsLocked = true
                }
            };

            ExcelUtils.ExportToExcel2003("D:\\", ls);



        }
    }
    
    public class Company
    {
  
        public int ID { get; set; }

        
        public string Name { get; set; }

        public string Code { get; set; }

        
        public int MaxNumberOfUser { get; set; }

        public bool IsDeleted { get; set; }

      
        public bool IsLocked { get; set; }
    }
}
