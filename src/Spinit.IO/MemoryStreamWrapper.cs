using System.IO;

namespace Spinit.IO
{
    public class MemoryStreamWrapper : Wrapper<MemoryStream>, IMemoryStream
    {
        public MemoryStreamWrapper(MemoryStream wrapped)
            : base(wrapped)
        {
        }

        public void Dispose()
        {
            Wrapped.Dispose();
        }
    }
}