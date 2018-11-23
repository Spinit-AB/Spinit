using System;
using System.Collections.Generic;
using System.Linq;

namespace Spinit.Utilities.Lists
{
    public class DoubleListAggregator<T1, T2> : ListBuilder<T1>
    {
        public DoubleListAggregator(IEnumerable<T1> firstList, IEnumerable<T2> secondList) : base(firstList)
        {
            SecondList = Build.ListOf(secondList);
        }

        protected IListBuilder<T2> SecondList { get; set; }

        public new TripleListAggregator<T1, T2, T3> And<T3>(IEnumerable<T3> thirdList)
        {
            return new TripleListAggregator<T1, T2, T3>(this, SecondList, thirdList);
        }

        public DoubleListAggregator<T1, T2> ForEach(Action<T1, T2> action)
        {
            return ForEach((first, second, index) => action(first, second));
        }

        public DoubleListAggregator<T1, T2> ForEach(Action<T1, T2, int> action)
        {
            var counter = 0;
            ForEach(first => SecondList.ForEach(second => action(first, second, counter++)));
            return this;
        }

        public ListBuilder<TReturn> ForEach<TReturn>(Func<T1, T2, TReturn> func)
        {
            return ForEach((first, second, index) => func(first, second));
        }

        public ListBuilder<TReturn> ForEach<TReturn>(Func<T1, T2, int, TReturn> func)
        {
            var counter = 0;
            var items = from first in this from second in SecondList select func(first, second, counter++);
            return new ListBuilder<TReturn>(items, IndexedPropertyName);
        }
    }
}