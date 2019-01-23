namespace Spinit.IO.Factories
{
    public interface IFileInfoFactory
    {
        IFileInfo New(string newFilePath);
    }
}