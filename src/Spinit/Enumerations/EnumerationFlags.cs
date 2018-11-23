using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Spinit.Enumerations
{
    public class EnumerationFlags : IEnumerable<Enumeration>
    {
        public virtual IEnumerable<Enumeration> Enumerations { get; protected set; }

        public static EnumerationFlags<TEnumeration> Create<TEnumeration>(params TEnumeration[] parameters)
            where TEnumeration : Enumeration<TEnumeration>
        {
            return new EnumerationFlags<TEnumeration>(parameters);
        }

        public static IEnumerable<Enumeration> ResolveBitwiseTo(Type enumerationType, long values)
        {
            return
                Enumeration.GetAll(enumerationType)
                           .Cast<Enumeration>()
                           .Where(enumeration => (values & enumeration.Value) == enumeration.Value)
                           .ToList();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((EnumerationFlags)obj);
        }

        public virtual IEnumerator<Enumeration> GetEnumerator()
        {
            return Enumerations.GetEnumerator();
        }

        public override int GetHashCode()
        {
            return Enumerations == null ? 0 : Convert.ToInt32(ToValue());
        }

        public virtual IEnumerable<long> GetValues()
        {
            return Enumerations.Select(x => x.Value);
        }

        public override string ToString()
        {
            var display = Enumerations == null
                              ? string.Empty
                              : string.Join(", ", Enumerations.Select(x => x.DisplayName));
            return string.Format("[ {0} ]", display);
        }

        public long ToValue()
        {
            return Enumerations.Any()
                       ? Enumerations.Select(x => x.Value).Aggregate((accumulator, next) => accumulator | next)
                       : 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected bool Equals(EnumerationFlags other)
        {
            return Equals(ToValue(), other.ToValue());
        }
    }
}