using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TsSoft.Commons.Collections;

namespace TsSoft.Commons.Test.Collections
{
    [TestClass]
    public class IteratorTest
    {
        [TestMethod]
        public void TestHasNextEmpty()
        {
            var iterator = new Iterator<DayOfWeek>(null as IEnumerable<DayOfWeek>);
            Assert.IsFalse(iterator.HasNext);
            iterator = new Iterator<DayOfWeek>(null as IEnumerator<DayOfWeek>);
            Assert.IsFalse(iterator.HasNext);
            iterator = new Iterator<DayOfWeek>(Enumerable.Empty<DayOfWeek>());
            Assert.IsFalse(iterator.HasNext);
        }

        [TestMethod]
        public void TestHasNext()
        {
            var collection = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
            var iterator = new Iterator<DayOfWeek>(collection.GetEnumerator());
            for (int i = 0; i < collection.Count(); i++)
            {
                Assert.IsTrue(iterator.HasNext);
                iterator.Next();
            }
            Assert.IsFalse(iterator.HasNext);
        }

        [TestMethod]
        public void TestNext()
        {
            IEnumerable<DayOfWeek> collection = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Friday };
            var iterator = new Iterator<DayOfWeek>(collection.GetEnumerator());
            Assert.AreEqual(DayOfWeek.Monday, iterator.Next());
            Assert.AreEqual(DayOfWeek.Friday, iterator.Next());
            ExceptionAssert.Throws<InvalidOperationException>(() => iterator.Next());
        }
    }
}