using System.Reflection;

namespace Spinit.Data.Export
{
    public interface IColumnIncluder
    {
        bool ShouldIncludeProperty(PropertyInfo propertyInfo);
    }
}