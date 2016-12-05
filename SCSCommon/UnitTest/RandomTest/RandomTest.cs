using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSCommon.RandomEx;

namespace UnitTest.RandomTest
{
    [TestFixture]
    class RandomTest
    {
        [Test]
        public void Test()
        {
            Assert.IsNotNull(RandomEx.GenerateRandomString(4, true));
         


        }
    }
}
