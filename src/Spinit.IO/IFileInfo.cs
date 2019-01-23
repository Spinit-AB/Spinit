using System.IO;

namespace Spinit.IO
{
    public interface IFileInfo : IFileSystemInfo<FileInfo>
    {
        IDirectoryInfo Directory { get; }

        string DirectoryName { get; }

        bool IsReadOnly { get; set; }

        long Length { get; }

        StreamWriter AppendText();

        IFileInfo CopyTo(string destFileName);

        IFileInfo CopyTo(string destFileName, bool overwrite);

        IFileStream Create();

        StreamWriter CreateText();

        void Decrypt();

        void Delete();

        void Encrypt();

        void MoveTo(string destFileName);

        IFileStream Open(FileMode open);

        IFileStream Open(FileMode mode, FileAccess access);

        IFileStream Open(FileMode mode, FileAccess access, FileShare share);

        IFileStream OpenRead();

        StreamReader OpenText();

        IFileStream OpenWrite();

        IFileInfo Replace(string destinationFileName, string destinationBackupFileName);

        IFileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);
    }
}
