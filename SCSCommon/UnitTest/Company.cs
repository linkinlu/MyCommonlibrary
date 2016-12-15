using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [Serializable]
    public class Company
    {

        public int ID { get; set; }


        public string Name { get; set; }


        public DateTime CreateTime { get; set; }

        public bool IsLocked { get; set; }

        public Department Department { get; set; }

        public CloseReason Reason { get; set; }

        public static Company GetSampleObj()
        {
            return new Company()
            {
                ID = 3,
                Name = "123",
                CreateTime = DateTime.Now,
                Department = new Department() {ID = 1, Name = "departmenta"},
                IsLocked = true,
                Reason = CloseReason.ExceptionError
            };
        }

        public static List<Company> GetSampleObjLst()
        {
            return new List<Company>()
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

        }
    }
    [Serializable]
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
