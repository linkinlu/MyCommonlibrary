using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSCommon.Convertion;
using SCSCommon.Strings;

namespace UnitTest.StringsTest
{
    [TestFixture]
    class StringsTest
    {
        [Test]
        public void Test()
        {
            var item = "你是一头猪 还是一只鸟 或者是一个人";
            var aa = item.ReplaceSensitiveChar(new string[] { "猪", "人" }, "*");



        }

    }
}
