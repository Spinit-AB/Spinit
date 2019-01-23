using System;
using System.Reflection;

namespace Spinit.Data.Export
{
    public class CustomColumnIncluder : IColumnIncluder
    {
        private readonly Func<PropertyInfo, bool> _shouldInclude;

        private CustomColumnIncluder(Func<PropertyInfo, bool> shouldInclude)
        {
            _shouldInclude = shouldInclude;
        }

        public static IColumnIncluder Create(Func<PropertyInfo, bool> shouldInclude)
        {
            return new CustomColumnIncluder(shouldInclude);
        }

        public bool ShouldIncludeProperty(PropertyInfo propertyInfo)
        {
            return _shouldInclude(propertyInfo);
        }
    }
}