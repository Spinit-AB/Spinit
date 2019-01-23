using System.IO;

namespace Spinit.IO
{
    public interface IFileStream : IStream<FileStream>
    {
        void Flush();

        void Close();

        void Write(byte[] buffer, int offset, int count);

        int ReadByte();
    }
}