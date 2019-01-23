using System.Collections.Generic;

using Spinit.Data.Export.Data;

namespace Spinit.Data.Export
{
    public interface IExportService
    {
        ITabularData CreateTabularData<T>(IEnumerable<T> rows);
    }
}