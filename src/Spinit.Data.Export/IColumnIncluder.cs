using System;
using System.Collections.Generic;
using System.Reflection;

namespace Spinit.Data.Export
{
    public interface IColumnIncluder
    {
        /// <summary>
        /// Determine if a property should be included or not
        /// </summary>
        bool ShouldIncludeProperty(PropertyInfo propertyInfo);

        /// <summary>
        /// Return available properties (in the order they will appear)
        /// </summary>
        IEnumerable<PropertyInfo> GetProperties(Type type);
    }
}