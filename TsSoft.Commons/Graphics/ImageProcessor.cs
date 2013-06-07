using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using System.IO;

namespace TsSoft.Commons.Graphics
{
    public class ImageProcessor
    {
        public byte[] Image { get; set; }

        //TODO get width original image
        public int Width { get; set; }

        //TODO get height original image
        public int Height { get; set; }

        /// <summary>
        /// Пропорциональное масштабирование изображения
        /// </summary>
        /// <param name="maxWidth">Максимальная ширина</param>
        /// <param name="maxHeight">Максимальная высота</param>
        public void Resize(int maxWidth, int maxHeight) 
        {
            using (var imageStream = new MemoryStream(Image))
            {
                var image = new Bitmap(imageStream);

                int originalWidth = image.Width;
                int originalHeight = image.Height;

                float ratioX = (float)maxWidth / (float)originalWidth;
                float ratioY = (float)maxHeight / (float)originalHeight;
                float ratio = Math.Min(ratioX, ratioY);

                int newWidth = (int)(originalWidth * ratio);
                int newHeight = (int)(originalHeight * ratio);

                Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(newImage))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                }
                using (var ms = new MemoryStream())
                {
                    newImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Image = ms.ToArray();
                    Width = newImage.Width;
                    Height = newImage.Height;
                }

            }
        }
    }
}