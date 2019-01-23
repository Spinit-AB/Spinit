using System.IO;

namespace Spinit.IO.Factories
{
    public class FileStreamFactory : IFileStreamFactory
    {
        public IFileStream New(string path, FileMode mode)
        {
            return new FileStreamWrapper(new FileStream(path, mode));
        }
    }
}