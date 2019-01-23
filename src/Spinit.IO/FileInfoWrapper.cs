using System.IO;

namespace Spinit.IO
{
    public class FileInfoWrapper : FileSystemInfoWrapper<FileInfo>, IFileInfo
    {
        public FileInfoWrapper(FileInfo fileInfo) : base(fileInfo)
        {
        }

        public long Length
        {
            get
            {
                return Wrapped.Length;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return Wrapped.IsReadOnly;
            }

            set
            {
                Wrapped.IsReadOnly = value;
            }
        }

        public IDirectoryInfo Directory
        {
            get
            {
                return Wrapped.Directory.Wrap();
            }
        }

        public string DirectoryName
        {
            get
            {
                return Wrapped.DirectoryName;
            }
        }

        public StreamWriter AppendText()
        {
            return Wrapped.AppendText();
        }

        public IFileInfo CopyTo(string destFileName)
        {
            return Wrapped.CopyTo(destFileName).Wrap();
        }

        public IFileStream Create()
        {
            return Wrapped.Create().Wrap();
        }

        public StreamWriter CreateText()
        {
            return Wrapped.CreateText();
        }

        public void Decrypt()
        {
            Wrapped.Decrypt();
        }

        public void Delete()
        {
            Wrapped.Delete();
        }

        public void Encrypt()
        {
            Wrapped.Encrypt();
        }

        public void MoveTo(string destFileName)
        {
            Wrapped.MoveTo(destFileName);
        }

        public IFileStream Open(FileMode open)
        {
            return Wrapped.Open(open).Wrap();
        }

        public IFileStream Open(FileMode mode, FileAccess access)
        {
            return Wrapped.Open(mode, access).Wrap();
        }

        public IFileStream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return Wrapped.Open(mode, access, share).Wrap();
        }

        public IFileStream OpenRead()
        {
            return Wrapped.OpenRead().Wrap();
        }

        public StreamReader OpenText()
        {
            return Wrapped.OpenText();
        }

        public IFileStream OpenWrite()
        {
            return Wrapped.OpenWrite().Wrap();
        }

        public IFileInfo Replace(string destinationFileName, string destinationBackupFileName)
        {
            return Wrapped.Replace(destinationFileName, destinationBackupFileName).Wrap();
        }

        public IFileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            return Wrapped.Replace(destinationFileName, destinationBackupFileName, ignoreMetadataErrors).Wrap();
        }

        public IFileInfo CopyTo(string destFileName, bool overwrite)
        {
            return new FileInfoWrapper(Wrapped.CopyTo(destFileName, overwrite));
        }
    }
}