using System.IO;

namespace Spinit.IO.Factories
{
    public class MemoryStreamFactory : IMemoryStreamFactory
    {
        public IMemoryStream New()
        {
            return new MemoryStreamWrapper(new MemoryStream());
        }
    }
}