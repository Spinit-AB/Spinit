using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Spinit.Enumerations
{
    /// <summary>
    /// An alternative to enum flags that allows for a displayname to be stored with each value
    /// </summary>
    public class EnumerationFlags<T> : EnumerationFlags, IEnumerable<T>
        where T : Enumeration<T>
    {
        public EnumerationFlags(params T[] initialCollection) : this((IEnumerable<T>)initialCollection) { }

        public EnumerationFlags(IEnumerable<T> initialCollection)
        {
            Flags = new List<T>(initialCollection);
        }

        public EnumerationFlags(IEnumerable<Enumeration> initialCollection)
        {
            var castItems = initialCollection.Cast<T>();
            Flags = new List<T>(castItems);
        }

        public EnumerationFlags()
        {
            Flags = new List<T>();
        }

        public int Count
        {
            get { return Flags.Count; }
        }

        public override IEnumerable<Enumeration> Enumerations
        {
            get { return Flags; }
        }

        protected IList<T> Flags { get; set; }

        public T this[int index]
        {
            get { return Flags[index]; }
        }

        public static EnumerationFlags<T> FromValue(long values)
        {
            var items = Enumeration.GetAll<T>().Where(enumeration => (values & enumeration.Value) == enumeration.Value);
            return new EnumerationFlags<T>(items);
        }

        public static long operator &(EnumerationFlags<T> flags, T flag)
        {
            return flags.ToValue() & flag.Value;
        }

        public void Add(T item)
        {
            Flags.Add(item);
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return Flags.GetEnumerator();
        }

        public bool Has(T item)
        {
            return Flags.Contains(item);
        }

        public bool Has(Func<T, bool> predicate)
        {
            return Flags.Any(predicate);
        }

        public bool HasAll(IEnumerable<T> items)
        {
            return items.All(Has);
        }

        public bool HasAny(IEnumerable<T> items)
        {
            return items.Any(Has);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}