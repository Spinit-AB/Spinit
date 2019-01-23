using System.IO;

namespace Spinit.IO.Factories
{
    public interface IStreamWriterFactory
    {
        IStreamWriter New<T>(IStream<T> ms) where T : Stream;
    }
}