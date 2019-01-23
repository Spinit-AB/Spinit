using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Spinit.Data.Export
{
    internal static class Utilities
    {
        internal static string GetDisplayNameOrPropertyName(PropertyInfo propertyInfo)
        {
            return GetDisplayName(propertyInfo) ?? propertyInfo.Name;
        }

        internal static string GetDisplayName(PropertyInfo propertyInfo)
        {
            var attr = (DisplayNameAttribute)propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();
            return (attr != null) ? attr.DisplayName : null;
        }

        internal static Type GetNullableType(Type t)
        {
            var returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }

            return returnType;
        }

        internal static bool IsNullableType(Type type)
        {
            return type == typeof(string)
                    || type.IsArray
                    || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
