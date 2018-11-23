using System;
using System.Collections.Generic;
using System.Linq;

namespace Spinit.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TType> Exclude<TType>(this IEnumerable<TType> list, params TType[] ignore)
        {
            return list.Where(item => !ignore.Contains(item));
        }

        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> @this, Func<T, object> projection)
        {
            return @this.GroupBy(projection).Select(x => x.First());
        }
    }
}