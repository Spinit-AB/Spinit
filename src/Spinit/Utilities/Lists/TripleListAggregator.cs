using System;
using System.Collections.Generic;
using System.Linq;

namespace Spinit.Utilities.Lists
{
    public class TripleListAggregator<T1, T2, T3> : DoubleListAggregator<T1, T2>
    {
        public TripleListAggregator(IEnumerable<T1> firstList, IEnumerable<T2> secondList, IEnumerable<T3> thirdList)
            : base(firstList, secondList)
        {
            ThirdList = Build.ListOf(thirdList);
        }

        protected IListBuilder<T3> ThirdList { get; set; }

        public TripleListAggregator<T1, T2, T3> ForEach(Action<T1, T2, T3> action)
        {
            return ForEach((first, second, third, index) => action(first, second, third));
        }

        public TripleListAggregator<T1, T2, T3> ForEach(Action<T1, T2, T3, int> action)
        {
            var counter = 0;
            ForEach((first, second) => ThirdList.ForEach(third => action(first, second, third, counter++)));
            return this;
        }

        public ListBuilder<TReturn> ForEach<TReturn>(Func<T1, T2, T3, TReturn> func)
        {
            return ForEach((first, second, third, index) => func(first, second, third));
        }

        public ListBuilder<TReturn> ForEach<TReturn>(Func<T1, T2, T3, int, TReturn> func)
        {
            var counter = 0;
            var items = from first in this
                from second in SecondList
                from third in ThirdList
                select func(first, second, third, counter++);
            return new ListBuilder<TReturn>(items, IndexedPropertyName);
        }
    }
}