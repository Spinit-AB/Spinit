using System.IO;

using OfficeOpenXml;

using Spinit.IO;

namespace Spinit.Data.Export.Implementations.Excel.EPPlusWrapping
{
    public class ExcelPackageWrapper : Wrapper<ExcelPackage>, IExcelPackage
    {
        public ExcelPackageWrapper(ExcelPackage excelPackage) : base(excelPackage)
        {
        }

        public ExcelWorkbook Workbook
        {
            get
            {
                return Wrapped.Workbook;
            }
        }

        public void SaveAs<T>(IStream<T> outputStream) where T : Stream
        {
            Wrapped.SaveAs(outputStream.UnWrap());
        }

        public byte[] GetAsByteArray()
        {
            return Wrapped.GetAsByteArray();
        }

        public void Dispose()
        {
            Wrapped.Dispose();
        }
    }
}