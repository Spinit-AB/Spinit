using System.IO;

namespace Spinit.IO
{
    public static class WrappingExtensions
    {
        public static IFileInfo Wrap(this FileInfo @this)
        {
            return new FileInfoWrapper(@this);
        }

        public static IDirectoryInfo Wrap(this DirectoryInfo @this)
        {
            return new DirectoryInfoWrapper(@this);
        }

        public static IFileStream Wrap(this FileStream @this)
        {
            return new FileStreamWrapper(@this);
        }
    }
}
