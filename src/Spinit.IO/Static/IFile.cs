namespace Spinit.IO.Static
{
    public interface IFile
    {
        void Delete(string path);
        bool Exists(string path);
        IFileStream Create(string path);
    }
}