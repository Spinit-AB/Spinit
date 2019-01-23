using System.Collections.Generic;

using Spinit.Data.Export.Implementations.Excel.EPPlusWrapping;

namespace Spinit.Data.Export.Implementations.Excel
{
    public class ExcelByteArrayExporter : ExcelExporter, IByteArrayExporter
    {
        public ExcelByteArrayExporter(IExportService exportService, ExcelPackageFactory excelPackageFactory)
            : base(exportService, excelPackageFactory) { }

        /// <summary>
        /// Creates a excel byte array exporter with default exportService and excelPackageFactory
        /// </summary>
        public static ExcelByteArrayExporter Create()
        {
            return new ExcelByteArrayExporter(new ExportService(), new ExcelPackageFactory());
        }

        public byte[] Write<T>(IEnumerable<T> list)
        {
            using (var excelPackage = ExcelPackageFactory.New())
            {
                var ds = ExportService.CreateTabularData(list);
                var excelWorksheet = DataTableToWorkSheet(excelPackage, ds);
                StyleExcelSheet(ds, excelWorksheet);

                return excelPackage.GetAsByteArray();
            }
        }
    }
}