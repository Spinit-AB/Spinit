using System;

namespace Spinit.IO
{
    public interface IStreamWriter : IDisposable
    {
        void Write(object value);
        void Flush();
    }
}