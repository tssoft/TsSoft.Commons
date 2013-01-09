using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace TsSoft.Commons.Graphics
{
    ///<author>Иван Ананьев</author>
    [TestClass()]
    public class ImageProcessorTest
    {
        [TestMethod()]
        public void ResizeTest()
        {
            var bitmap = new Bitmap(100, 100);
            ImageConverter converter = new ImageConverter();
            var arr = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            ImageProcessor target = new ImageProcessor { Image = arr };
            int maxWidth = 100;
            int maxHeight = 50;
            target.Resize(maxWidth, maxHeight);
            Assert.IsTrue(target.Width <= maxWidth);
            Assert.IsTrue(target.Height <= maxHeight);
            Assert.AreEqual(bitmap.Width / bitmap.Height, target.Width / target.Height);

            bitmap = new Bitmap(150, 250);
            arr = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            target.Image = arr;
            target.Resize(maxWidth, maxHeight);
            Assert.IsTrue(target.Width <= maxWidth);
            Assert.IsTrue(target.Height <= maxHeight);
            Assert.AreEqual(bitmap.Width / bitmap.Height, target.Width / target.Height);

            bitmap = new Bitmap(250, 100);
            arr = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            target.Image = arr;
            target.Resize(maxWidth, maxHeight);
            Assert.IsTrue(target.Width <= maxWidth);
            Assert.IsTrue(target.Height <= maxHeight);
            Assert.AreEqual(bitmap.Width / bitmap.Height, target.Width / target.Height);

            maxWidth = 50;
            maxHeight = 100;

            bitmap = new Bitmap(100, 100);
            arr = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            target.Image = arr;
            target.Resize(maxWidth, maxHeight);
            Assert.IsTrue(target.Width <= maxWidth);
            Assert.IsTrue(target.Height <= maxHeight);
            Assert.AreEqual(bitmap.Width / bitmap.Height, target.Width / target.Height);

            bitmap = new Bitmap(150, 250);
            arr = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            target.Image = arr;
            target.Resize(maxWidth, maxHeight);
            Assert.IsTrue(target.Width <= maxWidth);
            Assert.IsTrue(target.Height <= maxHeight);
            Assert.AreEqual(bitmap.Width / bitmap.Height, target.Width / target.Height);

            bitmap = new Bitmap(250, 100);
            arr = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            target.Image = arr;
            target.Resize(maxWidth, maxHeight);
            Assert.IsTrue(target.Width <= maxWidth);
            Assert.IsTrue(target.Height <= maxHeight);
            Assert.AreEqual(bitmap.Width / bitmap.Height, target.Width / target.Height);
        }
    }
}