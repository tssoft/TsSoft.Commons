using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TsSoft.Commons.Collections;

namespace TsSoft.Commons.Test.Collections
{
    [TestClass]
    public class EnumsTest
    {
        [TestMethod]
        public void TestStringToEnumWithDefault()
        {
            Assert.AreEqual(FileMode.Append, Enums.StringToEnum("Append", FileMode.Create));
            Assert.AreEqual(FileMode.Append, Enums.StringToEnum("ApPeNd", FileMode.Create));
            Assert.AreEqual(FileMode.Create, Enums.StringToEnum("hApPeNed", FileMode.Create));
            Assert.AreEqual(FileMode.Append, Enums.StringToEnum("Append", FileMode.Create, false));
            Assert.AreEqual(FileMode.Create, Enums.StringToEnum("ApPeNd", FileMode.Create, false));
        }
    }
}