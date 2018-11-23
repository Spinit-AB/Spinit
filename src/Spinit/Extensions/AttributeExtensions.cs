using System;
using System.Linq;
using System.Reflection;

namespace Spinit.Extensions
{
    public static class AttributeExtensions
    {
        public static bool HasAttribute<TAttribute>(this Type type)
        {
            return type.HasAttribute(typeof(TAttribute));
        }

        public static bool HasAttribute<TAttribute>(this MethodInfo methodInfo)
        {
            return methodInfo.HasAttribute(typeof(TAttribute));
        }

        public static bool HasAttribute<TAttribute>(this ParameterInfo parameterInfo)
        {
            return parameterInfo.HasAttribute(typeof(TAttribute));
        }

        public static bool HasAttribute(this Type type, Type attribute)
        {
            return type.GetTypeInfo().GetCustomAttributes(attribute, true).Any();
        }

        public static bool HasAttribute(this MethodInfo methodInfo, Type attribute)
        {
            return methodInfo.IsDefined(attribute);
        }

        public static bool HasAttribute(this ParameterInfo parameterInfo, Type attribute)
        {
            return parameterInfo.IsDefined(attribute);
        }
    }
}
