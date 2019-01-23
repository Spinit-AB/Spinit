using OfficeOpenXml;

namespace Spinit.Data.Export.Implementations.Excel.EPPlusWrapping
{
    public class ExcelPackageFactory
    {
        public IExcelPackage New()
        {
            return new ExcelPackageWrapper(new ExcelPackage());
        }
    }
}