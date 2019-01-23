using System.IO;

namespace Spinit.IO.Factories
{
    public class FileInfoFactory : IFileInfoFactory
    {
        public IFileInfo New(string newFilePath)
        {
            return new FileInfoWrapper(new FileInfo(newFilePath));
        }
    }
}