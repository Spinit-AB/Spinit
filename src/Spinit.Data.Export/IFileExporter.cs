using System.Collections.Generic;

namespace Spinit.Data.Export
{
    public interface IFileExporter
    {
        void Write<T>(IEnumerable<T> list, string outputPath);
    }
}