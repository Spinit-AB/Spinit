using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Spinit.Data.Export.Data.Implementation
{
    internal class ExportableData : ITabularData
    {
        private readonly DataTable _data;

        public ExportableData()
        {
            _data = new DataTable();
        }

        public List<IColumn> Columns
        {
            get
            {
                return _data.Columns.Cast<DataColumn>().Select(column => new ExportableColumn(column.ColumnName, column.DataType)).Cast<IColumn>().ToList();
            }
        }

        public List<IRow> Rows
        {
            get
            {
                return _data.Rows.Cast<DataRow>().Select(x => new ExportableRow(x)).Cast<IRow>().ToList();
            }
        }

        public DataTable DataTable 
        { 
            get
            {
                return _data;
            }
        }

        public void AddColumn(PropertyInfo propertyInfo)
        {
            _data.Columns.Add(new DataColumn(Utilities.GetDisplayNameOrPropertyName(propertyInfo), Utilities.GetNullableType(propertyInfo.PropertyType)));
        }

        public IRow AddRow()
        {
            return new ExportableRow(NewRow());
        }

        private DataRow NewRow()
        {
            var row = _data.NewRow();
            _data.Rows.Add(row);
            return row;
        }
    }
}