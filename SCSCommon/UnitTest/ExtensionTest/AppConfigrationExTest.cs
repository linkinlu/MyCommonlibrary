using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSCommon.Extension;
using SCSCommon.Log;

namespace UnitTest.ExtensionTest
{
    [TestFixture]
    class AppConfigrationExTest
    {
        [Test]
        public void log()
        {
            var data = AppConfigrationEx.GetValue<bool>("test",
                bool.Parse, () => false);
       

        }
    }
}
