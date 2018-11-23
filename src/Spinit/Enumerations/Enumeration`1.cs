using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Spinit.Enumerations
{
    /// <summary>
    /// An alternative to enum that allows for a displayname to be stored with each value
    /// </summary>
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class Enumeration<TEnumeration> : Enumeration, IComparable<TEnumeration>, IEquatable<TEnumeration>
        where TEnumeration : Enumeration<TEnumeration>
    {
        protected Enumeration(long value, string displayName) : base(value, displayName) { }

        public static bool Equals(long value, TEnumeration other)
        {
            return other.Value.Equals(value);
        }

        public static TEnumeration FromValue(long value)
        {
            return Parse(value, "value", item => item.Value.Equals(value));
        }

        public static TEnumeration[] GetAll()
        {
            return GetEnumerations();
        }

        public static TEnumeration Parse(string displayName)
        {
            return Parse(displayName, "display name", item => item.DisplayName == displayName);
        }

        public static void Switch(TEnumeration value, Action<EnumerationSwitchActionBuilder<TEnumeration>> @case, Action defaultAction = null)
        {
            var builder = new EnumerationSwitchActionBuilder<TEnumeration>(value, defaultAction);
            @case(builder);
            builder.Execute();
        }

        public static T Switch<T>(TEnumeration value, Action<EnumerationSwitchFunctionBuilder<TEnumeration, T>> @case, T @default)
        {
            var builder = new EnumerationSwitchFunctionBuilder<TEnumeration, T>(value, @default);
            @case(builder);
            return builder.Execute();
        }

        public static bool TryParse(long value, out TEnumeration result)
        {
            return TryParse(e => e.Value.Equals(value), out result);
        }

        public static bool TryParse(string displayName, out TEnumeration result)
        {
            return TryParse(e => e.DisplayName == displayName, out result);
        }

        public static EnumerationFlags<TEnumeration> operator &(Enumeration<TEnumeration> left, Enumeration<TEnumeration> right)
        {
            var items = new List<Enumeration<TEnumeration>> { left, right };
            return new EnumerationFlags<TEnumeration>(items);
        }

        public static EnumerationFlags<TEnumeration> operator &(EnumerationFlags<TEnumeration> left, Enumeration<TEnumeration> right)
        {
            var items = new List<Enumeration<TEnumeration>>(left) { right };
            return new EnumerationFlags<TEnumeration>(items);
        }

        public static bool operator ==(Enumeration<TEnumeration> left, Enumeration<TEnumeration> right)
        {
            return Equals(left, right);
        }

        public static bool operator ==(long left, Enumeration<TEnumeration> right)
        {
            if (right == null)
            {
                return false;
            }

            return left == right.Value;
        }

        public static bool operator !=(Enumeration<TEnumeration> left, Enumeration<TEnumeration> right)
        {
            return !Equals(left, right);
        }

        public static bool operator !=(long left, Enumeration<TEnumeration> right)
        {
            return !(left == right);
        }

        public int CompareTo(TEnumeration other)
        {
            return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TEnumeration);
        }

        public bool Equals(TEnumeration other)
        {
            return other != null && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool IsAny(params TEnumeration[] possibleMatches)
        {
            return possibleMatches.Any() && possibleMatches.Any(possibleMatch => possibleMatch.Value == Value);
        }

        public override sealed string ToString()
        {
            return DisplayName;
        }

        private static TEnumeration[] GetEnumerations()
        {
            var enumerationType = typeof(TEnumeration);
            return
                enumerationType.GetTypeInfo().GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                               .Where(info => enumerationType.GetTypeInfo().IsAssignableFrom(info.FieldType))
                               .Select(info => info.GetValue(null))
                               .Cast<TEnumeration>()
                               .ToArray();
        }

        private static TEnumeration Parse(object value, string description, Func<TEnumeration, bool> predicate)
        {
            TEnumeration result;

            if (!TryParse(predicate, out result))
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(TEnumeration));
                throw new ArgumentException(message, "value");
            }

            return result;
        }

        private static bool TryParse(Func<TEnumeration, bool> predicate, out TEnumeration result)
        {
            result = GetAll().FirstOrDefault(predicate);
            return result != null;
        }
    }
}