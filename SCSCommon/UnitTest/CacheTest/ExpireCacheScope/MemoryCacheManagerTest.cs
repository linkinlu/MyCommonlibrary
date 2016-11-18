using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSCommon.Cache.ExpireCacheScope;
using SCSCommon.Convertion;
using UnitTest.ConvertionTest;

namespace UnitTest.CacheTest.ExpireCacheScope
{
    [TestFixture]
   public class MemoryCacheManagerTest
    {

        [Test]
        public void ToTest()
        {
            BaseCacheExpireManager cache = new MemoryCacheManager();


            cache.Add("BB", new TestA() { A = 1 }, new TimeSpan(0, 0, 60),new List<string>() {"b"});
            cache.Add("AA", new TestA() { A = 1 }, new TimeSpan(0, 0, 60), new List<string>() { "a" });
            cache.Add("CC", new TestA() {A = 1}, new TimeSpan(0, 0, 60), new List<string>() {"a","b"});
            cache.Add("DD", new TestA() {A = 1}, new TimeSpan(0, 0, 60), new List<string>() {"a", "b", "c", "d"});

            cache.RemoveByTags(new List<string>() {"a"});
            Assert.IsNotNull(cache.Get<TestA>("BB"));
            Assert.IsNull(cache.Get<TestA>("AA"));
            Assert.IsNotNull(cache.Get<TestA>("CC"));

            cache.RemoveByTags(new List<string>() {"c", "d"});

            Assert.IsNull(cache.Get<TestA>("DD"));

            //Cache Expire
            cache.Add("aaa", new TestA() {A = 1}, new TimeSpan(0, 0, 1));

            Assert.IsNotNull(cache.Get<TestA>("aaa"));

            Thread.Sleep(2000);

            Assert.IsNull(cache.Get<TestA>("aaa"));


        }
    }

   
}
