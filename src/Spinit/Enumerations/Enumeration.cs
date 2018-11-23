using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Spinit.Enumerations
{
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class Enumeration : IComparable
    {
        private readonly string _displayName;
        private readonly long _value;

        protected Enumeration() { }

        protected Enumeration(long value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        public long Value
        {
            get { return _value; }
        }

        public static long AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
            return absoluteDifference;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }

        public static T FromValue<T>(long value) where T : Enumeration
        {
            var matchingItem = Parse<T, long>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        public static IEnumerable<T> FromValues<T>(IEnumerable<long> values) where T : Enumeration
        {
            return values.Select(FromValue<T>);
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var type = typeof(T);
            var fields = type.GetTypeInfo().GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);
            return fields.Select(info => info.GetValue(null)).OfType<T>();
        }

        public static IEnumerable GetAll(Type type)
        {
            var fields = type.GetTypeInfo().GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);
            return fields.Select(info => info.GetValue(null));
        }

        public static bool operator ==(long left, Enumeration right)
        {
            if (right == null)
            {
                return false;
            }

            return left == right.Value;
        }

        public static bool operator !=(long left, Enumeration right)
        {
            return !(left == right);
        }

        public virtual int CompareTo(object other)
        {
            return Value.CompareTo(((Enumeration)other).Value);
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = _value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return DisplayName;
        }

        private static T Parse<T, TK>(TK value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new EnumerationParseException(message);
            }

            return matchingItem;
        }
    }
}