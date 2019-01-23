using System.IO;

namespace Spinit.IO
{
    public interface IDirectoryInfo : IFileSystemInfo<DirectoryInfo>
    {
        IDirectoryInfo Parent { get; }

        IDirectoryInfo Root { get; }

        void Create();

        void Delete(bool recursive);

        IFileInfo[] GetFiles();

        IFileInfo[] GetFiles(string searchPattern);

        IFileInfo[] GetFiles(string searchPattern, SearchOption searchOption);
    }
}