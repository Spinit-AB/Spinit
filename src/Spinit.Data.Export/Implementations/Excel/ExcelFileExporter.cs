using System.Collections.Generic;
using System.IO;

using Spinit.Data.Export.Implementations.Excel.EPPlusWrapping;
using Spinit.IO.Factories;

namespace Spinit.Data.Export.Implementations.Excel
{
    public class ExcelFileExporter : ExcelExporter, IFileExporter
    {
        private readonly IFileStreamFactory _fileStreamFactory;

        public ExcelFileExporter(IExportService exportService, ExcelPackageFactory excelPackageFactory, IFileStreamFactory fileStreamFactory)
            : base(exportService, excelPackageFactory)
        {
            _fileStreamFactory = fileStreamFactory;
        }

        /// <summary>
        /// Creates a excel file exporter with default exportService, excelPackageFactory and fileStreamFactory
        /// </summary>
        public static ExcelFileExporter Create()
        {
            return new ExcelFileExporter(new ExportService(), new ExcelPackageFactory(), new FileStreamFactory());
        }

        public void Write<T>(IEnumerable<T> list, string outputPath)
        {
            var table = ExportService.CreateTabularData(list);
            using (var excelPackage = ExcelPackageFactory.New())
            {
                var excelWorksheet = DataTableToWorkSheet(excelPackage, table);
                StyleExcelSheet(table, excelWorksheet);
                using (var fileStream = _fileStreamFactory.New(outputPath, FileMode.Create))
                {
                    excelPackage.SaveAs(fileStream);    
                }
            }
        }
    }
}