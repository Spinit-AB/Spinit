using System.IO;

namespace Spinit.IO
{
    public interface IMemoryStream : IStream<MemoryStream>
    {
        new MemoryStream UnWrap();
    }
}