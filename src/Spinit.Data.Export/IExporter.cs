using System.Collections.Generic;

namespace Spinit.Data.Export
{
    public interface IExporter<out TReturn>
    {
        TReturn Write<T>(IEnumerable<T> list);
    }
}