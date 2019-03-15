using System;
using System.Collections.Generic;
using System.Reflection;

namespace Spinit.Data.Export
{
    public class DefaultColumnIncluder : IColumnIncluder
    {
        protected Dictionary<PropertyInfo, bool> shouldIncludeColumnCache = new Dictionary<PropertyInfo, bool>();

        public virtual IEnumerable<PropertyInfo> GetProperties(Type type) => type.GetProperties();

        public virtual bool ShouldIncludeProperty(PropertyInfo propertyInfo)
        {
            if (!shouldIncludeColumnCache.ContainsKey(propertyInfo))
            {
                shouldIncludeColumnCache[propertyInfo] = !Attribute.IsDefined(propertyInfo, typeof(ExcludeExportAttribute));
            }

            return shouldIncludeColumnCache[propertyInfo];  
        }
    }
}