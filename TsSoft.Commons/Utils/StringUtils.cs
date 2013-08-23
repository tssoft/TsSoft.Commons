namespace TsSoft.Commons.Utils
{
    using System.Linq;

    public static class StringUtils
    {
        /// <summary>
        /// Returns first not null string, otherwise null
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static string FirstNotNull(params string[] strings)
        {
            if (strings == null)
            {
                return null;
            }
            return strings.FirstOrDefault(x => x != null);
        }

        /// <summary>
        /// Returns first not null and not empty string.
        /// If there is none such string, it returns first not null string.
        /// If there is none such string, it returns null
        /// </summary>
        public static string FirstNotEmpty(params string[] strings)
        {
            if (strings == null)
            {
                return null;
            }
            var result = strings.FirstOrDefault(x => !string.IsNullOrEmpty(x));
            if (result == null)
            {
                result = FirstNotNull(strings);
            }
            return result;
        }

        /// <summary>
        /// Returns first not null, not empty string consisting not only of white-space characters.
        /// If there is none such string, it returns first not null and not empty string.
        /// If there is none such string, it returns first not null string.
        /// If there is none such string, it returns null
        /// </summary>
        public static string FirstNotWhitespace(params string[] strings)
        {
            if (strings == null)
            {
                return null;
            }
            var result = strings.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
            if (result == null)
            {
                result = FirstNotEmpty(strings);
            }
            return result;
        }
    }
}