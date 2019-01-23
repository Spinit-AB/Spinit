namespace Spinit.IO.Factories
{
    public interface IDirectoryInfoFactory
    {
        IDirectoryInfo New(string path);
    }
}