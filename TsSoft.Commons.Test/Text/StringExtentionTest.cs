using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsSoft.Commons.Text;

namespace TsSoft.Commons.Test.Text
{
    [TestClass]
    public class StringExtentionTest
    {
        [TestMethod]
        public void TestToNormalCase()
        {
            Assert.AreEqual("", "".ToNormalCase());
            Assert.AreEqual("normal case", "Normal case".ToNormalCase());
            Assert.AreEqual(" \tnormal case", " \tNormal case".ToNormalCase());
            Assert.AreEqual("normal Case", "Normal Case".ToNormalCase());
            // TODO Assert.AreEqual("HTML page", "HTML page".ToNormalCase());

        }
    }
}