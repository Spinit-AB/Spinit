using System.IO;

namespace Spinit.IO.Factories
{
    public class DirectoryInfoFactory : IDirectoryInfoFactory
    {
        public IDirectoryInfo New(string path)
        {
            return new DirectoryInfoWrapper(new DirectoryInfo(path));
        }
    }
}