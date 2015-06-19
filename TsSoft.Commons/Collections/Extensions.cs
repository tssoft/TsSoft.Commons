namespace TsSoft.Commons.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Extensions
    {
        public static IEnumerable<T> Compact<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");
            return source.Where(t => t != null);
        }

        public static IEnumerable<string> Compact(this IEnumerable<string> source)
        {
            if (source == null) throw new ArgumentNullException("source");
            return source.Where(t => !string.IsNullOrEmpty(t));
        }
    }
}