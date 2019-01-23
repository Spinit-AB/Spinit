using System.Globalization;
using System.Linq;
using System.Threading;

using OfficeOpenXml;
using OfficeOpenXml.Style;

using Spinit.Data.Export.Data;
using Spinit.Data.Export.Data.Implementation;
using Spinit.Data.Export.Implementations.Excel.EPPlusWrapping;

namespace Spinit.Data.Export.Implementations.Excel
{
    public class ExcelExporter
    {
        public ExcelExporter(IExportService exportService, ExcelPackageFactory excelPackageFactory)
        {
            ExcelPackageFactory = excelPackageFactory;
            ExportService = exportService;
            CultureInfo = Thread.CurrentThread.CurrentCulture;
        }

        public CultureInfo CultureInfo { get; set; }

        protected ExcelPackageFactory ExcelPackageFactory { get; set; }
        protected IExportService ExportService { get; set; }

        protected virtual void StyleExcelSheet(ITabularData dataTable, ExcelWorksheet excelWorksheet)
        {
            var numberCols = dataTable.Columns.Count();
            StyleHeader(excelWorksheet.Cells[1, 1, 1, numberCols]);

            for (int i = 0; i < numberCols; i++)
            {
                var col = dataTable.Columns[i];
                excelWorksheet.Column(i + 1).AutoFit();

                using (var excelRange = excelWorksheet.Cells[2, i + 1, 2 + dataTable.Rows.Count, i + 2])
                {
                    if (ColumnIsNumeric(col))
                    {
                        StyleNumberCellRange(excelRange);
                    }

                    if (ColumnIsDateTime(col))
                    {
                        StyleDateTimeCellRange(excelRange);
                    }
                }
            }
        }

        protected virtual void StyleHeader(ExcelRange range)
        {
            range.Style.Font.Bold = true;
        }

        protected virtual void StyleNumberCellRange(ExcelRange range)
        {
            range.Style.Numberformat.Format = "#,##0.00";
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
        }

        protected virtual void StyleDateTimeCellRange(ExcelRange range)
        {
            range.Style.Numberformat.Format = CultureInfo.DateTimeFormat.ShortDatePattern;
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
        }

        protected virtual bool ColumnIsNumeric(IColumn col)
        {
            return col.DataType.FullName == "System.Decimal"
                        || col.DataType.FullName == "System.Double"
                        || col.DataType.FullName == "System.Int16"
                        || col.DataType.FullName == "System.Int32"
                        || col.DataType.FullName == "System.Int64";
        }

        protected virtual bool ColumnIsDateTime(IColumn col)
        {
            return col.DataType.FullName == "System.DateTime";
        }

        protected ExcelWorksheet DataTableToWorkSheet(IExcelPackage excelPackage, ITabularData table)
        {
            var excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

            // This is cheating. We cannot cast table to ExportableData.
            excelWorksheet.Cells["A1"].LoadFromDataTable(((ExportableData)table).DataTable, true);
            return excelWorksheet;
        }   
    }
}