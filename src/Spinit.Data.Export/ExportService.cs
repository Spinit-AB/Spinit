using System;
using System.Collections.Generic;

using Spinit.Data.Export.Data;
using Spinit.Data.Export.Data.Implementation;

namespace Spinit.Data.Export
{
    public class ExportService : IExportService
    {
        public ExportService()
        {
            ColumnIncluder = new DefaultColumnIncluder();
        }

        public IColumnIncluder ColumnIncluder { get; set; }

        public ITabularData CreateTabularData<T>(IEnumerable<T> rows)
        {
            var dt = CreateTable();
            WriteHeaderRow(dt, typeof(T));
            WriteRows(dt, rows);
            return dt;
        }

        private ITabularData CreateTable()
        {
            return new ExportableData();
        }

        private void WriteHeaderRow(ITabularData data, Type type)
        {
            foreach (var info in type.GetProperties())
            {
                if (ColumnIncluder.ShouldIncludeProperty(info))
                {
                    data.AddColumn(info);
                }
            }
        }

        private void WriteRows<T>(ITabularData table, IEnumerable<T> rows)
        {
            foreach (var row in rows)
            {
                WriteRow(table, row);
            }
        }

        private void WriteRow<T>(ITabularData data, T dataRow)
        {
            var row = data.AddRow();
            foreach (var info in dataRow.GetType().GetProperties())
            {
                if (ColumnIncluder.ShouldIncludeProperty(info))
                {
                    if (!Utilities.IsNullableType(info.PropertyType))
                    {
                        row[info] = info.GetValue(dataRow, null);
                    }
                    else
                    {
                        row[info] = info.GetValue(dataRow, null) ?? DBNull.Value;
                    }
                }
            }
        }
    }
}