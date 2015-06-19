namespace TsSoft.Commons.Test.Collections
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TsSoft.Commons.Collections;

    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void CompactGenericTest()
        {
            var collection = new Collection<TestClass>
            {
                null,
                new TestClass(),
                new TestClass(),
                null,
                new TestClass(),
                null,
            };
            Assert.AreEqual(6, collection.Count);
            Assert.AreEqual(3, collection.Count(tc => tc != null));
            Assert.AreEqual(3, collection.Count(tc => tc == null));
            var compactCollection = collection.Compact();
            Assert.AreEqual(3, compactCollection.Count());
            Assert.AreEqual(3, compactCollection.Count(tc => tc != null));
            Assert.AreEqual(0, compactCollection.Count(tc => tc == null));
        }

        [TestMethod]
        public void CompactStringTest()
        {
            const string notEmpty = "NOT_EMPTY";
            var collection = new Collection<string>
            {
                null,
                notEmpty,
                notEmpty,
                string.Empty,
                "",
                " ",
            };
            Assert.AreEqual(6, collection.Count);
            Assert.AreEqual(3, collection.Count(string.IsNullOrEmpty));
            Assert.AreEqual(3, collection.Count(tc => !string.IsNullOrEmpty(tc)));
            var compactCollection = collection.Compact();
            Assert.IsNotNull(compactCollection);
            Assert.AreEqual(3, compactCollection.Count());
            Assert.AreEqual(3, compactCollection.Count(tc => !string.IsNullOrEmpty(tc)));
            Assert.AreEqual(0, compactCollection.Count(string.IsNullOrEmpty));
        }

        private class TestClass
        {
        }
    }
}