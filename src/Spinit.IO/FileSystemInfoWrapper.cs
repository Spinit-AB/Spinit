using System;
using System.IO;

namespace Spinit.IO
{
    public abstract class FileSystemInfoWrapper<T> : Wrapper<T>, IFileSystemInfo<T> where T : FileSystemInfo
    {
        protected FileSystemInfoWrapper(T fileSystemInfo) : base(fileSystemInfo)
        {
        }

        public FileAttributes Attributes
        {
            get
            {
                return Wrapped.Attributes;
            }

            set
            {
                Wrapped.Attributes = value;
            }
        }

        public DateTime CreationTime
        {
            get
            {
                return Wrapped.CreationTime;
            }

            set
            {
                Wrapped.CreationTime = value;
            }
        }

        public DateTime CreationTimeUtc
        {
            get
            {
                return Wrapped.CreationTimeUtc;
            }

            set
            {
                Wrapped.CreationTimeUtc = value;
            }
        }

        public bool Exists
        {
            get
            {
                return Wrapped.Exists;
            }
        }

        public string Extension
        {
            get
            {
                return Wrapped.Extension;
            }
        }

        public string FullName
        {
            get
            {
                return Wrapped.FullName;
            }
        }

        public DateTime LastWriteTimeUtc
        {
            get
            {
                return Wrapped.LastWriteTimeUtc;
            }

            set
            {
                Wrapped.LastWriteTimeUtc = value;
            }
        }

        public DateTime LastAccessTime
        {
            get
            {
                return Wrapped.LastAccessTime;
            }

            set
            {
                Wrapped.LastAccessTime = value;
            }
        }

        public DateTime LastAccessTimeUtc
        {
            get
            {
                return Wrapped.LastAccessTimeUtc;
            }

            set
            {
                Wrapped.LastAccessTimeUtc = value;
            }
        }

        public DateTime LastWriteTime
        {
            get
            {
                return Wrapped.LastWriteTime;
            }

            set
            {
                Wrapped.LastWriteTime = value;
            }
        }

        public string Name
        {
            get { return Wrapped.Name; }
        }
    }
}