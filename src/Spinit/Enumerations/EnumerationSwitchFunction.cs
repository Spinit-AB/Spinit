using System;

namespace Spinit.Enumerations
{
    public class EnumerationSwitchFunction<TEnumeration, T>
        where TEnumeration : Enumeration
    {
        public EnumerationSwitchFunction(TEnumeration value, Func<TEnumeration, T> function)
        {
            Value = value;
            Function = function;
        }

        public Func<TEnumeration, T> Function { get; set; }
        public TEnumeration Value { get; set; }

        public T Execute()
        {
            return Function(Value);
        }
    }
}