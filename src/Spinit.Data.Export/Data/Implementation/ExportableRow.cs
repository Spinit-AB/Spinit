using System.Data;
using System.Reflection;

namespace Spinit.Data.Export.Data.Implementation
{
    internal class ExportableRow : IRow
    {
        private readonly DataRow _dataRow;

        public ExportableRow(DataRow dataRow)
        {
            _dataRow = dataRow;
        }

        public object this[PropertyInfo propertyInfo]
        {
            get { return _dataRow[Utilities.GetDisplayNameOrPropertyName(propertyInfo)]; }
            set { _dataRow[Utilities.GetDisplayNameOrPropertyName(propertyInfo)] = value; }
        }

        public object this[string columnName]
        {
            get { return _dataRow[columnName]; }
            set { _dataRow[columnName] = value; }
        } 
    }
}
