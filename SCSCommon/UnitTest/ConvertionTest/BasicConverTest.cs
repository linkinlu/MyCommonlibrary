using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSCommon.Convertion;

namespace UnitTest.ConvertionTest
{
    [TestFixture]
   public class BasicConverTest
    {

        [Test]
        public void ToTest()
        {
            var result = BasicConvert.To<int>("123");
            Assert.AreEqual(result, 123);

            var resultA = BasicConvert.To<int>("asd");
            Assert.AreEqual(result, 123);

        }
    }
}
