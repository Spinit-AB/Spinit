using System;

namespace Spinit.Enumerations
{
    public class EnumerationParseException : Exception
    {
        public EnumerationParseException(string message) : base(message) { }
    }
}
