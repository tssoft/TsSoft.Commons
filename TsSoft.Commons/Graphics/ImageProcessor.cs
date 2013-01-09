using System;
using System.Drawing;
using System.IO;

namespace TsSoft.Commons.Graphics
{
    public class ImageProcessor
    {
        public byte[] Image { get; set; }

        public int Width { get; set; }

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
                var bt = new Bitmap(imageStream);
                int resizedWidth = maxWidth;
                int resizedHeight = maxHeight;
                if (bt.Width <= maxWidth && bt.Height <= maxHeight)
                {
                    resizedWidth = bt.Width;
                    resizedHeight = bt.Height;
                }
                else
                {
                    if (bt.Width < bt.Height)
                    {
                        var calcWidth = (int)(bt.Width * resizedHeight / bt.Height);
                        if (calcWidth <= maxWidth)
                        {
                            resizedWidth = calcWidth;
                        }
                        else
                        {
                            resizedHeight = resizedHeight * resizedWidth / calcWidth;
                        }
                    }
                    else
                    {
                        if (bt.Width > bt.Height)
                        {
                            var calcHeight = bt.Height * resizedWidth / bt.Width;
                            if (calcHeight <= maxHeight)
                            {
                                resizedHeight = calcHeight;
                            }
                            else
                            {
                                resizedWidth = resizedWidth * resizedHeight / calcHeight;
                            }
                        }
                        else
                        {
                            resizedWidth = (int)Math.Min((decimal)maxHeight, (decimal)maxWidth);
                            resizedHeight = resizedWidth;
                        }
                    }
                }
                Image img = bt.GetThumbnailImage(
                    resizedWidth,
                    resizedHeight,
                    delegate { return false; },
                    IntPtr.Zero);
                using (var ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Image = ms.ToArray();
                    Width = img.Width;
                    Height = img.Height;
                }
            }
        }
    }
}