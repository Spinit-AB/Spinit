using System.Collections;
using System.Collections.Generic;

namespace Spinit
{
    /// <summary>
    /// An Enumerable list structure that can contain sorting and paging information
    /// </summary>
    public interface IExtendedEnumerable<out TEntity> : IEnumerable<TEntity>, IExtendedEnumerable { }

    public interface IExtendedEnumerable : IEnumerable
    {
        long TotalCount { get; }
        int Page { get; }
        int PageSize { get; }
        int NumberOfPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        string SortedBy { get; }
        bool SortedAscending { get; }
        bool IsPaged { get; }
        bool IsSorted { get; }
    }
}