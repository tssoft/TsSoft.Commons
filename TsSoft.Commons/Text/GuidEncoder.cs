using System;
using System.Globalization;
using System.Linq;

namespace TsSoft.Commons.Text
{
    /// <summary>
    /// «Сокращатель» для Guid, приводящий их к 28-разрядному виду: YQEDGZ2ZQUMUUGYYGI673Z05WDNR
    /// В отличие от реализации на основе Base64 является регистронезависимым
    /// Например: http://www.singular.co.nz/blog/archive/2007/12/20/shortguid-a-shorter-and-url-friendly-guid-in-c-sharp.aspx
    /// Или: http://madskristensen.net/post/A-shorter-and-URL-friendly-GUID.aspx
    /// Ну и: http://stackoverflow.com/questions/1032376/guid-to-base64-for-url
    /// </summary>
    public static class GuidEncoder
    {
        private static char[] hextAlphabet = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }.Reverse().ToArray();

        public static string Encode(Guid guid)
        {
            string source = guid.ToString().Replace("-", "").ToLower();
            var encoded = longToHext(long.Parse(source.Substring(0, 8), NumberStyles.HexNumber), string.Empty, 6);
            encoded += longToHext(long.Parse(source.Substring(8, 8), NumberStyles.HexNumber), string.Empty, 6);
            encoded += longToHext(long.Parse(source.Substring(16, 8), NumberStyles.HexNumber), string.Empty, 6);
            encoded += longToHext(long.Parse(source.Substring(24, 8), NumberStyles.HexNumber), string.Empty, 6);
            return encoded.ToLower();
        }

        public static Guid Decode(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return Guid.Empty;
            }
            if (source.IsGuid())
            {
                return Guid.Parse(source);
            }
            var hext = source.ToLower();
            long l = hextToLong(source.ToLower(), 0, 6);
            var decoded = string.Format("{0:X8}{1:X8}{2:X8}{3:X8}",
                hextToLong(hext.Substring(0, 7), 0, 6),
                hextToLong(hext.Substring(7, 7), 0, 6),
                hextToLong(hext.Substring(14, 7), 0, 6),
                hextToLong(hext.Substring(21, 7), 0, 6));
            return Guid.ParseExact(decoded, "N");
        }

        private static string longToHext(long partialLong, string partialHext, int digit)
        {
            long power = (long)Math.Pow(hextAlphabet.Length, digit);
            long index = partialLong / power;
            var hext = partialHext + hextAlphabet[index];
            if (digit > 0)
            {
                hext = longToHext(partialLong % power, hext, digit - 1);
            }
            return hext;
        }

        private static long hextToLong(string partialHext, long partialLong, int digit)
        {
            long power = (long)Math.Pow(hextAlphabet.Length, digit);
            long index = Array.IndexOf(hextAlphabet, partialHext[0]);
            long value = partialLong + index * power;
            if (digit > 0)
            {
                value = hextToLong(partialHext.Substring(1), value, digit - 1);
            }
            return value;
        }
    }
}