using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSCommon.DateTimeExt;
namespace UnitTest.DateTimeTest
{
    [TestFixture]
    public class DateTimeTest
    {
        [Test]
        public void Test()
        {
            var items = DateTime.Now.ToHHmmss();

            var items1 = DateTime.Now.ToyyyyMMddHHmmssfffffff();
            var items2 = DateTime.Now.ToyyyyMMddHHmm();

            var items3 = DateTime.Now.ToHHmmss();
        }
    }
}
