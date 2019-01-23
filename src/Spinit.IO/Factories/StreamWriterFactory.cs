using System.IO;

namespace Spinit.IO.Factories
{
    public class StreamWriterFactory : IStreamWriterFactory
    {
        public IStreamWriter New<T>(IStream<T> ms) where T : Stream
        {
            return new StreamWriterWrapper(new StreamWriter(ms.UnWrap()));
        }
    }
}