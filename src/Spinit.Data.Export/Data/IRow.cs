using System.Reflection;

namespace Spinit.Data.Export.Data
{
    public interface IRow
    {
        object this[string columnName] { get; set; }
        object this[PropertyInfo propertyInfo] { get; set; }
    }
}