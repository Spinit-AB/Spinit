using System.Collections.Generic;
using System.IO;
using System.Text;

using Spinit.IO.Factories;

namespace Spinit.Data.Export.Implementations.Csv
{
    public class CsvFileExporter : CsvExporter, IFileExporter
    {
        private readonly IFileStreamFactory _fileStreamFactory;

        public CsvFileExporter(IExportService exportService, IFileStreamFactory fileStreamFactory) : base(exportService)
        {
            _fileStreamFactory = fileStreamFactory;
        }

        /// <summary>
        /// Creates a csv file exporter with default exportService and fileStreamFactory
        /// </summary>
        public static CsvFileExporter Create()
        {
            return new CsvFileExporter(new ExportService(), new FileStreamFactory());
        }

        public void Write<T>(IEnumerable<T> list, string outputPath)
        {
            var ds = ExportService.CreateTabularData(list);
            var bytes = Encoding.Unicode.GetBytes(Create(ds));
            using (var fileStream = _fileStreamFactory.New(outputPath, FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Flush();
            }
        }
    }
}