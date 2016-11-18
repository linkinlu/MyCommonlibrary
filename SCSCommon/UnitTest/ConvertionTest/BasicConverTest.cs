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
            var test = new TestA();
            var result = test.To<TestA, ITest>();
            Assert.AreEqual(1, result.sett());

            var testbb = "1";
            var resultbb = testbb.To(0);
            Assert.AreEqual(1, resultbb);

            var testcc = "CCC";
            var resultCC = testcc.To(0);
            Assert.AreNotEqual(1, resultCC);

            var testdd = new TestB();
            var resultDD = testdd.To<TestB, BaseTest>();
            Assert.AreEqual(0, resultDD.GetB());


            var testee = new TestB();
            var testff = new TestA();
            var resultee = testff.To<TestA, BaseTest>();
            Assert.AreEqual(0, resultDD.GetB());
        }
    }

    public class TestA:ITest
    {
        public int A { get; set; }

        public int sett()
        {
            return 1;
        }
    }

    public interface ITest
    {
        int sett();
    }

    public abstract class BaseTest
    {
        public virtual int GetB()
        {
            return 0;
        }
    }

    public class TestB : BaseTest
    {

        //public override int GetB()
        //{
        //    return 1;
        //}
    }
}
