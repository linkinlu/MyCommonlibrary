using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSCommon.Log;

namespace UnitTest.LogTest
{
    [TestFixture]
    class LogTest
    {
        [Test]
        public void log()
        {
            LogUtil.LogException("123", new Exception() {});

            LogUtil.LogInfo("这是一个测试");
        }
    }
}
