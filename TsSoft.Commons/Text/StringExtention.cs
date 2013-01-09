using System.Text.RegularExpressions;

namespace TsSoft.Commons.Text
{
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
    }
}