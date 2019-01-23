using System.IO;
using System.Linq;

namespace Spinit.IO
{
    public class DirectoryInfoWrapper : FileSystemInfoWrapper<DirectoryInfo>, IDirectoryInfo
    {
        public DirectoryInfoWrapper(DirectoryInfo directoryInfo) : base(directoryInfo)
        {
        }

        public IDirectoryInfo Parent
        {
            get
            {
                return Wrapped.Parent.Wrap();
            }
        }

        public IDirectoryInfo Root
        {
            get
            {
                return Wrapped.Root.Wrap();
            }
        }

        public void Create()
        {
            Wrapped.Create();
        }

        public void Delete(bool recursive)
        {
            Wrapped.Delete(recursive);
        }

        public IFileInfo[] GetFiles()
        {
            return Wrapped.GetFiles().Select(x => x.Wrap()).ToArray();
        }

        public IFileInfo[] GetFiles(string searchPattern)
        {
            return Wrapped.GetFiles(searchPattern).Select(x => x.Wrap()).ToArray();
        }

        public IFileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
        {
            return Wrapped.GetFiles(searchPattern, searchOption).Select(x => x.Wrap()).ToArray();
        }
    }
}