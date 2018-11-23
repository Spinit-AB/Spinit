using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Spinit
{
    /// <summary>
    /// An Enumerable list structure that can contain sorting and paging information
    /// </summary>
    public class ExtendedEnumerable<TModel> : IExtendedEnumerable<TModel> where TModel : class
    {
        public ExtendedEnumerable() : this(new List<TModel>()) { }

        public ExtendedEnumerable(IEnumerable<TModel> items)
            : this(items, items.Count(), -1, -1, null, true, false, false) { }

        public ExtendedEnumerable(IEnumerable<TModel> items, string sortedBy, bool sortedAscending)
            : this(items, items.Count(), -1, -1, sortedBy, sortedAscending, false, true) { }

        public ExtendedEnumerable(IEnumerable<TModel> items, long totalCount, int page, int pageSize)
            : this(items, totalCount, page, pageSize, null, true, true, false) { }

        public ExtendedEnumerable(IEnumerable<TModel> items, long totalCount, int page, int pageSize, string sortedBy, bool sortedAscending)
            : this(items, totalCount, page, pageSize, sortedBy, sortedAscending, true, true) { }

        public ExtendedEnumerable(IEnumerable<TModel> items, long totalCount, int page, int pageSize, string sortedBy, bool sortedAscending, bool isPaged, bool isSorted)
        {
            Items = items.ToList();

            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
            NumberOfPages = GetNumberOfPages();
            HasPreviousPage = Page > 1;
            HasNextPage = Page < NumberOfPages;

            SortedBy = sortedBy;
            SortedAscending = sortedAscending;

            IsPaged = isPaged;
            IsSorted = isSorted;
        }

        public long TotalCount { get; protected set; }
        public int Page { get; protected set; }
        public int PageSize { get; protected set; }
        public int NumberOfPages { get; protected set; }
        public bool HasPreviousPage { get; protected set; }
        public bool HasNextPage { get; protected set; }
        public string SortedBy { get; protected set; }
        public bool SortedAscending { get; protected set; }
        public bool IsPaged { get; private set; }
        public bool IsSorted { get; private set; }
        protected ICollection<TModel> Items { get; set; }

        public IEnumerator<TModel> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public virtual void Add(TModel model)
        {
            Items.Add(model);
            TotalCount = Items.Count;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        protected virtual int GetNumberOfPages()
        {
            if (PageSize <= 0)
            {
                return -1;
            }

            var calculatedNumberOfPages = (int)(TotalCount / PageSize);
            var hasEmptyOrUnevenPage = calculatedNumberOfPages == 0 || TotalCount % PageSize > 0;
            return hasEmptyOrUnevenPage ? calculatedNumberOfPages + 1 : calculatedNumberOfPages;
        }
    }
}