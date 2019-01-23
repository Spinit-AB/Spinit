using System.IO;

namespace Spinit.IO.Static
{
    public class FileWrap : IFile
    {
        public void Delete(string path)
        {
            File.Delete(path);
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public IFileStream Create(string path)
        {
            return File.Create(path).Wrap();
        }
    }
}