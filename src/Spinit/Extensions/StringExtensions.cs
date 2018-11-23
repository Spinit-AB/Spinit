using System.Globalization;
using System.Linq;

namespace Spinit.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static string FormatWith(this string format, params object[] values)
        {
            return string.IsNullOrEmpty(format) ?
                string.Empty : string.Format(format, values);
        }

        public static string Reverse(this string value)
        {
            var charArray = value.ToCharArray().Reverse().ToArray();
            return new string(charArray);
        }

        public static decimal ToDecimalOrDefault(this string textValue, decimal defaultValue = default(decimal))
        {
            return decimal.TryParse(textValue.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal output) ? output : defaultValue;
        }

        public static decimal ToDecimal(this string textValue)
        {
            return decimal.Parse(textValue.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        public static int ToIntOrDefault(this string textValue, int defaultValue = default(int))
        {
            return int.TryParse(textValue, out int output) ? output : defaultValue;
        }

        public static int ToInt(this string textValue)
        {
            return int.Parse(textValue);
        }

        public static bool ToBoolOrDefault(this string textValue, bool defaultValue = default(bool))
        {
            return bool.TryParse(textValue, out bool output) ? output : defaultValue;
        }

        public static bool ToBool(this string textValue)
        {
            return bool.Parse(textValue);
        }
    }
}