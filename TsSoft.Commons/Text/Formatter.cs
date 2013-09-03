using System;
using System.Text;

namespace TsSoft.Commons.Text
{
    public class Formatter
    {
        public static string FormatFileSize(long bytes, int scale = 2)
        {
            scale = scale < 0 ? 2 : scale;
            const int factor = 1024;
            string[] orders = new string[] { "ГБ", "МБ", "КБ", "байт" };
            long max = (long)Math.Pow(factor, orders.Length - 1);
            foreach (string order in orders)
            {
                if (bytes > max)
                {
                    var formatString = "{0:F" + scale + "} {1}";
                    return string.Format(formatString, decimal.Divide(bytes, max), order);
                }
                max /= factor;
            }
            return "пустой";
        }

        /// <summary>
        /// Возвращает не более maxCharacters символов, при этом не обрезая слова
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxCharacters"></param>
        /// <returns></returns>
        public static string ShortenTextByWord(string text, int maxCharacters)
        {
            if (text == null || text.Length <= maxCharacters || maxCharacters <= 0)
            {
                return text;
            }
            var lastDelimeterPos = text.LastIndexOfAny(new char[] { ' ', ',', ';', '\t', '\n', '\r' }, maxCharacters - 1, maxCharacters);
            if (lastDelimeterPos < 0)
            {
                lastDelimeterPos = maxCharacters;
            }
            return new StringBuilder()
                .Append(text.Substring(0, lastDelimeterPos))
                .Append("…")
                .ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="maxCharacters"></param>
        /// <returns></returns>
        ///<author>Оксана Хижняк</author>
        public static string ShortenUrlByLength(string url, int maxCharacters = 15)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return url;
            }
            var result = url;
            result = result.Replace("http://", string.Empty).Replace("https://", string.Empty);
            if (result.EndsWith("/"))
            {
                result = result.Remove(result.Length - 1);
            }
            if (result.Length > maxCharacters)
            {
                result = result.Substring(0, maxCharacters);
                result += '…';
            }
            return result;
        }

        /*
        public static string ShortenTextToWidth(string text, Font font, int maxWidth)
        {
            if (string.IsNullOrEmpty(text) || (maxWidth > TextRenderer.MeasureText(text, font).Width))
            {
                return text;
            }

            string fileName = Path.GetFileName(text);
            string pathSeparator = "..." + Path.DirectorySeparatorChar;
            int textWidth = 0;
            if (TextRenderer.MeasureText(pathSeparator + fileName, font).Width > maxWidth)
            {
                string trimmedName = fileName;
                string newName = trimmedName;
                do
                {
                    int index = trimmedName.Length / 2;
                    trimmedName = trimmedName.Remove(index, 1);
                    newName = trimmedName.Insert(index, "...");
                    textWidth = TextRenderer.MeasureText(newName, font).Width;
                } while (textWidth > maxWidth);
                return newName;
            }

            pathSeparator = "…" + Path.DirectorySeparatorChar;
            string rightPart = pathSeparator + fileName;
            string leftPart = text.Remove(text.Length - rightPart.Length + 1);
            string trimmedPath = string.Empty;
            int trim = 1;
            do
            {
                trimmedPath = leftPart.Remove(leftPart.Length - trim) + rightPart;
                textWidth = TextRenderer.MeasureText(trimmedPath, font).Width;
                trim++;
            } while (textWidth > maxWidth);
            return trimmedPath;
        }
        */
    }
}