using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Spinit.Utilities.Lists
{
    public class ListBuilder<T> : IListBuilder<T>
    {
        private readonly List<T> _list;

        public ListBuilder(string indexedPropertyName = "Name") : this()
        {
            _list = new List<T>();
            IndexedPropertyName = indexedPropertyName;
        }

        public ListBuilder(IEnumerable<T> initialCollection, string indexedPropertyName = "Name") : this()
        {
            _list = new List<T>(initialCollection);
            IndexedPropertyName = indexedPropertyName;
        }

        protected ListBuilder()
        {
            Random = new Random();
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public string IndexedPropertyName { get; protected set; }

        protected Random Random { get; set; }

        public T this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        public T this[string name]
        {
            get
            {
                var nameProperty = typeof(T).GetTypeInfo().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                             .SingleOrDefault(x => x.Name == IndexedPropertyName);
                if (nameProperty == null)
                {
                    return default(T);
                }

                foreach (var item in _list)
                {
                    var propertyValue = (string)nameProperty.GetValue(item);
                    if (propertyValue == name)
                    {
                        return item;
                    }
                }

                return default(T);
            }
        }

        public IListBuilder<T> Append(T item)
        {
            _list.Add(item);
            return this;
        }

        public IListBuilder<T> Append(IEnumerable<T> items)
        {
            _list.AddRange(items);
            return this;
        }

        public IListBuilder<T> Append<TEntity>(IEnumerable<TEntity> entities, Func<TEntity, T> createItem)
        {
            _list.AddRange(entities.Select(createItem));
            return this;
        }

        public IListBuilder<T> Append<TEntity>(IEnumerable<TEntity> entities, Func<TEntity, int, T> createItem)
        {
            _list.AddRange(entities.Select(createItem));
            return this;
        }

        public IListBuilder<TReturn> ForEach<TReturn>(Func<T, TReturn> func)
        {
            return new ListBuilder<TReturn>(IndexedPropertyName).Append(this.Select(func));
        }

        public IListBuilder<TReturn> ForEach<TReturn>(Func<T, int, TReturn> func)
        {
            return new ListBuilder<TReturn>(IndexedPropertyName).Append(this.Select(func));
        }

        public IListBuilder<T> ForEach(Action<T> action)
        {
            foreach (var item in _list)
            {
                action(item);
            }

            return this;
        }

        public IListBuilder<T> ForEach(Action<T, int> action)
        {
            var count = 0;
            foreach (var item in _list)
            {
                action(item, count++);
            }

            return this;
        }

        public IListBuilder<T> Where(Func<T, bool> predicate)
        {
            return new ListBuilder<T>(_list.Where(predicate), IndexedPropertyName);
        }

        public IListBuilder<TReturn> OfType<TReturn>()
        {
            return new ListBuilder<TReturn>(_list.OfType<TReturn>(), IndexedPropertyName);
        }

        public T GetRandom(Random random = null)
        {
            if (Count == 0)
            {
                throw new Exception("Cannot return a random item becuase List is empty.");
            }

            var randomIndex = (random ?? Random).Next(0, Count);
            return _list[randomIndex];
        }

        public IListBuilder<T> GetRandomItems(int? quantity = null, Random random = null)
        {
            var output = new ListBuilder<T>(IndexedPropertyName);
            if (Count == 0)
            {
                return output;
            }

            var numberOfItems = quantity ?? (random ?? Random).Next(1, Count + 1);
            output.Append(Shuffle()
                .Take(numberOfItems));

            return output;
        }

        public IListBuilder<T> Shuffle(Random random = null)
        {
            var rng = random ?? Random;
            var n = Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var value = this[k];
                this[k] = this[n];
                this[n] = value;
            }

            return this;
        }

        public DoubleListAggregator<T, T2> And<T2>(IEnumerable<T2> items)
        {
            return new DoubleListAggregator<T, T2>(this, items);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }
    }
}