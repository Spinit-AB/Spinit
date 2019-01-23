using System.IO;

namespace Spinit.IO
{
    public class FileStreamWrapper : Wrapper<FileStream>, IFileStream
    {
        public FileStreamWrapper(FileStream fileStream) : base(fileStream)
        {
        }

        public void Flush()
        {
            Wrapped.Flush();
        }

        public void Flush(bool flushToDisk)
        {
            Wrapped.Flush(flushToDisk);
        }

        public void Close()
        {
            Wrapped.Close();
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            Wrapped.Write(buffer, offset, count);
        }

        public int ReadByte()
        {
            return Wrapped.ReadByte();
        }

        public void Dispose()
        {
            Wrapped.Dispose();
        }
    }
}