using System;
using System.Linq;
using System.Reflection;

namespace Spinit.Extensions
{
    public static class EnumExtensions
    {
        public static T Next<T>(this T source) where T : struct
        {
            if (!typeof(T).GetTypeInfo().IsEnum)
            {
                throw new ArgumentException(string.Format("Argument {0} is Not of enum type", typeof(T).FullName));
            }

            var enumArray = (T[])Enum.GetValues(source.GetType());
            var j = Array.IndexOf(enumArray, source) + 1;
            return (enumArray.Length == j) ? enumArray[0] : enumArray[j];
        }

        public static T? Next<T>(this T? source) where T : struct
        {
            if (source == null)
            {
                return null;
            }

            if (!typeof(T).GetTypeInfo().IsEnum)
            {
                throw new ArgumentException(string.Format("Argument {0} is Not of enum type", typeof(T).FullName));
            }

            var enumArray = (T[])Enum.GetValues(source.GetType());
            var j = Array.IndexOf(enumArray, source) + 1;
            return (enumArray.Length == j) ? enumArray[0] : enumArray[j];
        }

        public static T SkipItems<T>(this T src, int numberOfSkips) where T : struct
        {
            var allEnumValues = (T[])Enum.GetValues(src.GetType());
            var enumTopIndex = allEnumValues.Count();
            var skipIndex = numberOfSkips % enumTopIndex;
            return allEnumValues[skipIndex];
        }

        public static bool HasFlag<T>(this T value, T flagToCheckFor) where T : struct
        {
            var fl = Convert.ToInt32(value);
            var opt = Convert.ToInt32(flagToCheckFor);
            return (fl & opt) == opt;
        }
    }
}