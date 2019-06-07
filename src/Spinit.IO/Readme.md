# Documentation

## Mockable file system

### The idea

When you want to write Unit Tests on code that relies on the .Net System.IO library you usually run into the problem of having to access the actual file system. To remedy this, instead of using FileInfo and DirectoryInfo, use IFileInfo and IDirectoryInfo of Spinit.IO. (More interfaces can be added as needed). Spinit.IO also comes with implementations to these interfaces; FileInfoWrapper and DirectoryInfoWrapper respectively. These implementations mirrors all members of their System.IO counterparts and should not be used directly. You should be working with the IFileInfo and IDirectoryInfo interfaces.

### How to use

#### Factories

So to make your code testable, everytime you think you need a FileInfo or DirectoryInfo object, you should think again. Instead you should inject a factory to create IFileInfo and IDirectoryInfo objects for you. (Remember, the new keyword destroys testability.)

    public MyFileHandlingClass(IFileInfoFactory factory)
    {
        _factory = factory
    }

    public void CreateFile(string fileName)
    {
        _factory.New(fileName).Create();
    }

Now, when writing tests for this class, create a stub/fake/mock/whatever to implement the IFileInfoFactory and your unit tests will no longer rely on the file system. You can even mock IFileInfo/IDirectoryInfo if you want to assert that certain methods were invoked on them.

#### Static wrappers

System.IO also comes with a lot of static File-related methods. For example

    File.Delete(path)

Instead, inject an IFile into your object

    public MyConstructor(IFile file)
    {
        _file = file;
    }

And use it like

    _file.Delete(path);

### StructureMap

Use [Spinit.IO.StructureMap](Spinit.IO.StructureMap) to autoregister all the factories.

### Overcoming obstacles

When using third party libraries etc. that exposes System.IO classes that does not conform to the Spinit.IO interfaces, you should of course wrap them in your own interfaces, but in case you don't have time for that, we provide a set of extension methods to quickly wrap a System.IO object in an Spinit.IO wrapper.

    new FileInfo("C:\importantfiles\1.txt").Wrap();

The wrapper classes also comes with UnWrap() methods in case your third party library requires a FileInfo or DirectoryInfo object.

    IFileInfo fileInfo = _factory.New(fileName);
    _thirdPartyLibrary.NeedsAFile(fileInfo.UnWrap());
