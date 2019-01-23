using System;
using System.IO;

namespace Spinit.IO
{
    public interface IStream<out T> : IDisposable where T : Stream
    {
        T UnWrap();
    }
}