namespace TsSoft.Commons.Text
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtention
    {
        public static bool IsGuid(this string expression)
        {
            if (expression != null)
            {
                Regex guidRegEx = new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");
                return guidRegEx.IsMatch(expression);
            }
            return false;
        }

        /// <summary>
        /// "Sentence case" -> "sentence case"
        /// </summary>
        public static string ToNormalCase(this string original)
        {
            if (string.IsNullOrEmpty(original))
            {
                return original;
            }
            var trimmed = original.TrimStart();
            var nonSpaceChar = trimmed[0];
            var nonSpaceCharIndex = original.IndexOf(nonSpaceChar);
            // TODO Abbreviation support
            return original.ReplaceAt(nonSpaceCharIndex, Char.ToLowerInvariant(nonSpaceChar));
        }

        public static string ReplaceAt(this string original, int index, char newChar)
        {
            if (original == null || index >= original.Length)
            {
                return original;
            }
            var builder = new StringBuilder(original);
            builder[index] = newChar;
            return builder.ToString();
        }
    }
}