namespace TsSoft.Commons.Utils
{
    public static class StringUtils
    {
        public static string DefaultString(string primaryString, string defaultString)
        {
            return string.IsNullOrEmpty(primaryString) ? defaultString : primaryString;
        }
    }
}