using System.Collections.Generic;

namespace Spinit.Enumerations
{
    public static class EnumerationFlagsExtensions
    {
        public static EnumerationFlags<T> ToFlags<T>(this IEnumerable<T> items) where T : Enumeration<T>
        {
            return items == null ? new EnumerationFlags<T>() : new EnumerationFlags<T>(items);
        }
    }
}