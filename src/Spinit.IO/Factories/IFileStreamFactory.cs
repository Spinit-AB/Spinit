using System.IO;

namespace Spinit.IO.Factories
{
    public interface IFileStreamFactory
    {
        IFileStream New(string path, FileMode mode);
    }
}