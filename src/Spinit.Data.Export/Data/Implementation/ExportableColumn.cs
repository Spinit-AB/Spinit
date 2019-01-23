using System;

namespace Spinit.Data.Export.Data.Implementation
{
    internal class ExportableColumn : IColumn
    {
        public ExportableColumn(string columnName, Type dataType)
        {
            ColumnName = columnName;
            DataType = dataType;
        }

        public string ColumnName { get; set; }
        public Type DataType { get; set; }
    }
}