using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSCommon.Clone;
using UnitTest.OfficeTest;

namespace UnitTest.Clone
{
    [TestFixture]
    class MemoryCloneTest
    {
        [Test]
        public void test()
        {
            var com = Company.GetSampleObj();
           var com2 =  MemoryDeepClone.DeepClone<Company>(com);
            Assert.IsTrue(!com.Equals(com2));


            var com3 = JsonDeepClone.DeepClone(com);
            Assert.IsTrue(!com.Equals(com3));
        }
    }
}
