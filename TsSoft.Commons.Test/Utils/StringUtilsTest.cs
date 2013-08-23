namespace TsSoft.Commons.Test.Utils
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TsSoft.Commons.Utils;

    [TestClass]
    public class StringUtilsTest
    {
        [TestMethod]
        public void TestDefaultString()
        {
            Assert.AreEqual("2", StringUtils.DefaultString(null, "2"));
            Assert.AreEqual("2", StringUtils.DefaultString("", "2"));
            Assert.AreEqual(" ", StringUtils.DefaultString(" ", "2"));
            Assert.AreEqual("1", StringUtils.DefaultString("1", "2"));
        }
    }
}
