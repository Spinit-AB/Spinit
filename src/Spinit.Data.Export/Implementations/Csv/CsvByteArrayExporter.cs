using System.Collections.Generic;
using System.Text;

namespace Spinit.Data.Export.Implementations.Csv
{
    public class CsvByteArrayExporter : CsvExporter, IByteArrayExporter
    {
        public CsvByteArrayExporter(IExportService exportService) 
            : base(exportService)
        {
        }

        /// <summary>
        /// Creates a csv byte array exporter with default exportService
        /// </summary>
        public static CsvByteArrayExporter Create()
        {
            return new CsvByteArrayExporter(new ExportService());
        }

        public byte[] Write<T>(IEnumerable<T> list)
        {
            var ds = ExportService.CreateTabularData(list);
            return Encoding.Unicode.GetBytes(Create(ds));
        }
    }
}