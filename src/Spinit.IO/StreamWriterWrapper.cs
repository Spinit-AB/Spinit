using System.IO;

namespace Spinit.IO
{
    public class StreamWriterWrapper : Wrapper<StreamWriter>, IStreamWriter
    {
        public StreamWriterWrapper(StreamWriter wrapped)
            : base(wrapped)
        {
        }

        public void Dispose()
        {
            Wrapped.Dispose();
        }

        public void Write(object value)
        {
            Wrapped.Write(value);
        }

        public void Flush()
        {
            Wrapped.Flush();
        }
    }
}