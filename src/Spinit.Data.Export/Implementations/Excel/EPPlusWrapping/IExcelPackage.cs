using System;
using System.IO;

using OfficeOpenXml;

using Spinit.IO;

namespace Spinit.Data.Export.Implementations.Excel.EPPlusWrapping
{
    public interface IExcelPackage : IDisposable
    {
        ExcelWorkbook Workbook { get; }

        void SaveAs<T>(IStream<T> outputStream) where T : Stream;

        byte[] GetAsByteArray();
    }
}
