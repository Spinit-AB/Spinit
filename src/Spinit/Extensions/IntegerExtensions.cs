using System;

namespace Spinit.Extensions
{
    public static class IntegerExtensions
    {
        public static T ToEnum<T>(this int value) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static bool IsEven(this int value)
        {
            return (value % 2) == 0;
        }

        public static bool IsOdd(this int value)
        {
            return !value.IsEven();
        }
    }
}