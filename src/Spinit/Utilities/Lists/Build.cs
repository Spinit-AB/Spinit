using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Spinit.Extensions;

namespace Spinit.Utilities.Lists
{
    public static class Build
    {
        public static IListBuilder<T> ListOf<T>(Expression<Func<T, string>> indexedProperty) where T : class
        {
            return new ListBuilder<T>(indexedProperty.GetName());
        }

        public static IListBuilder<T> ListOf<T>()
        {
            return new ListBuilder<T>();
        }

        public static IListBuilder<T> ListOf<T>(IEnumerable<T> initialCollection)
        {
            return new ListBuilder<T>(initialCollection);
        }

        public static IListBuilder<T> ListOf<T>(IEnumerable<T> initialCollection, 
                                                Expression<Func<T, string>> indexedProperty) where T : class
        {
            return new ListBuilder<T>(initialCollection, indexedProperty.GetName());
        }
    }
}