using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Spinit.Enumerations
{
    public class EnumerationSwitchActionBuilder<TEnumeration> : IEnumerable<EnumerationSwitchAction<TEnumeration>>
        where TEnumeration : Enumeration
    {
        private readonly Action _defaultAction;
        private readonly List<EnumerationSwitchAction<TEnumeration>> _list;
        private readonly TEnumeration _value;

        public EnumerationSwitchActionBuilder(TEnumeration value, Action defaultAction = null)
        {
            _value = value;
            _defaultAction = defaultAction;
            _list = new List<EnumerationSwitchAction<TEnumeration>>();
        }

        public void Execute()
        {
            var match = _list.FirstOrDefault(item => item.Value == _value);
            if (match == null && _defaultAction != null)
            {
                _defaultAction();
            }
            else if (match != null)
            {
                match.Execute();
            }
        }

        public IEnumerator<EnumerationSwitchAction<TEnumeration>> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public void Of(TEnumeration value, Action<TEnumeration> then)
        {
            _list.Add(new EnumerationSwitchAction<TEnumeration>(value, then));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}