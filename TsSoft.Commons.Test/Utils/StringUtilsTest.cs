namespace TsSoft.Commons.Test.Utils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TsSoft.Commons.Utils;

    [TestClass]
    public class StringUtilsTest
    {
        [TestMethod]
        public void TestFirstNotNull()
        {
            Assert.IsNull(StringUtils.FirstNotNull());
            Assert.IsNull(StringUtils.FirstNotNull(null));
            Assert.IsNull(StringUtils.FirstNotNull(null, null));
            Assert.AreEqual("2", StringUtils.FirstNotNull(null, "2"));
            Assert.AreEqual("", StringUtils.FirstNotNull("", "2"));
            Assert.AreEqual(" ", StringUtils.FirstNotNull(" ", "2"));
            Assert.AreEqual("1", StringUtils.FirstNotNull("1", "2"));
        }

        [TestMethod]
        public void TestFirstNotEmpty()
        {
            Assert.IsNull(StringUtils.FirstNotEmpty());
            Assert.IsNull(StringUtils.FirstNotEmpty(null));
            Assert.IsNull(StringUtils.FirstNotEmpty(null, null));
            Assert.AreEqual("2", StringUtils.FirstNotEmpty(null, "2"));
            Assert.AreEqual("2", StringUtils.FirstNotEmpty("", "2"));
            Assert.AreEqual(" ", StringUtils.FirstNotEmpty(" ", "2"));
            Assert.AreEqual("1", StringUtils.FirstNotEmpty("1", "2"));
            Assert.AreEqual("2", StringUtils.FirstNotEmpty("", null, "2"));
            Assert.AreEqual(" ", StringUtils.FirstNotEmpty("", null, " ", "2"));
        }

        [TestMethod]
        public void TestFirstNotWhitespace()
        {
            Assert.IsNull(StringUtils.FirstNotWhitespace());
            Assert.IsNull(StringUtils.FirstNotWhitespace(null));
            Assert.IsNull(StringUtils.FirstNotWhitespace(null, null));
            Assert.AreEqual("2", StringUtils.FirstNotWhitespace(null, "2"));
            Assert.AreEqual("2", StringUtils.FirstNotWhitespace("", "2"));
            Assert.AreEqual("2", StringUtils.FirstNotWhitespace(" ", "2"));
            Assert.AreEqual("1", StringUtils.FirstNotWhitespace("1", "2"));
            Assert.AreEqual("2", StringUtils.FirstNotWhitespace("", null, "2"));
            Assert.AreEqual(" ", StringUtils.FirstNotWhitespace(null, "", " "));
        }
    }
}