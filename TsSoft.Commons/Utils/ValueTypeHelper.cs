namespace TsSoft.Commons.Utils
{
    /// <summary>
    /// http://www.deanchalk.me.uk/post/Is-It-Nullable-.aspx
    /// </summary>
    public static class ValueTypeHelper
    {
        public static bool IsNullable<T>(T t)
        {
            return false;
        }

        public static bool IsNullable<T>(T? t) where T : struct
        {
            return true;
        }
    }
}