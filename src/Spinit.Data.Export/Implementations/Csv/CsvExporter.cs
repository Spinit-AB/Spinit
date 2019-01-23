using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

using Spinit.Data.Export.Data;

namespace Spinit.Data.Export.Implementations.Csv
{
    public class CsvExporter
    {
        protected CsvExporter(IExportService exportService)
        {
            ExportService = exportService;
            Separator = ",";
            CultureInfo = Thread.CurrentThread.CurrentCulture;
        }

        public CultureInfo CultureInfo { get; set; }
        public string Separator { get; set; }
        protected IExportService ExportService { get; set; }

        protected string Create(ITabularData table)
        {
            var header = string.Join(Separator, table.Columns.Select(x => x.ColumnName).ToArray());
            var csvdata = new StringBuilder().AppendLine(header);

            foreach (IRow row in table.Rows)
            {
                csvdata.AppendLine(ToCsvFields(table, row));
            }

            return csvdata.ToString();
        }

        private string ToCsvFields(ITabularData table, IRow row)
        {
            var line = new StringBuilder();
            foreach (var column in table.Columns)
            {
                if (line.Length > 0)
                {
                    line.Append(Separator);
                }

                line.Append(FormattedOutput(row[column.ColumnName]));
            }

            return line.ToString();
        }

        private string FormattedOutput(object value)
        {
            string str;

            if (value is decimal)
            {
                str = ((decimal)value).ToString("F2", CultureInfo);
            }
            else if (value is int)
            {
                str = ((int)value).ToString("F0", CultureInfo);
            }
            else if (value is DateTime)
            {
                str = ((DateTime)value).ToString("d", CultureInfo);
            }
            else
            {
                str = value != null ? string.Format("\"{0}\"", value) : "\"\"";
            }

            return string.IsNullOrEmpty(str) ? string.Empty : str;
        }
    }
}