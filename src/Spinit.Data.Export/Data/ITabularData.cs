using System.Collections.Generic;
using System.Reflection;

namespace Spinit.Data.Export.Data
{
    public interface ITabularData
    {
        List<IColumn> Columns { get; }
        List<IRow> Rows { get; }

        IRow AddRow();
        void AddColumn(PropertyInfo propertyInfo);
    }
}