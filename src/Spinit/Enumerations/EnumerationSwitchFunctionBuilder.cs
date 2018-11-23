using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Spinit.Enumerations
{
    public class EnumerationSwitchFunctionBuilder<TEnumeration, T> : IEnumerable<EnumerationSwitchFunction<TEnumeration, T>>
        where TEnumeration : Enumeration
    {
        private readonly T _default;
        private readonly List<EnumerationSwitchFunction<TEnumeration, T>> _list;
        private readonly TEnumeration _value;

        public EnumerationSwitchFunctionBuilder(TEnumeration value, T @default)
        {
            _default = @default;
            _value = value;
            _list = new List<EnumerationSwitchFunction<TEnumeration, T>>();
        }

        public T Execute()
        {
            var match = _list.FirstOrDefault(item => item.Value == _value);
            return match != null ? match.Execute() : _default;
        }

        public IEnumerator<EnumerationSwitchFunction<TEnumeration, T>> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public void Of(TEnumeration value, Func<TEnumeration, T> then)
        {
            _list.Add(new EnumerationSwitchFunction<TEnumeration, T>(value, then));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}