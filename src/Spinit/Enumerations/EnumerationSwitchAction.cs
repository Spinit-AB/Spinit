using System;

namespace Spinit.Enumerations
{
    public class EnumerationSwitchAction<TEnumeration>
        where TEnumeration : Enumeration
    {
        public EnumerationSwitchAction(TEnumeration value, Action<TEnumeration> action)
        {
            Value = value;
            Action = action;
        }

        public Action<TEnumeration> Action { get; set; }
        public TEnumeration Value { get; set; }
        public void Execute()
        {
            Action(Value);
        }
    }
}