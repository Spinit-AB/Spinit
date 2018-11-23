using System;
using System.Collections.Generic;

namespace Spinit.Utilities.Lists
{
    public interface IListBuilder<T> : IList<T>
    {
        string IndexedPropertyName { get; }
        T this[string name] { get; }
        IListBuilder<T> Append(T item);
        IListBuilder<T> Append(IEnumerable<T> items);
        IListBuilder<T> Append<TEntity>(IEnumerable<TEntity> entities, Func<TEntity, T> createItem);
        IListBuilder<T> Append<TEntity>(IEnumerable<TEntity> entities, Func<TEntity, int, T> createItem);
        IListBuilder<TReturn> ForEach<TReturn>(Func<T, TReturn> func);
        IListBuilder<TReturn> ForEach<TReturn>(Func<T, int, TReturn> func);
        IListBuilder<T> ForEach(Action<T> action);
        IListBuilder<T> ForEach(Action<T, int> action);
        IListBuilder<T> Where(Func<T, bool> predicate);
        IListBuilder<TReturn> OfType<TReturn>();
        T GetRandom(Random random = null);
        IListBuilder<T> GetRandomItems(int? quantity = null, Random random = null);
        IListBuilder<T> Shuffle(Random random = null);
        DoubleListAggregator<T, T2> And<T2>(IEnumerable<T2> items);
    }
}