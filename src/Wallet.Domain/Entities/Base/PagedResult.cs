using System;
using System.Collections.Generic;

namespace Wallet.Domain.Entities.Base;

public class PagedResult<T>
{
    public IList<T> Data { get; init; }
    public int CurrentPage { get; init; }
    public int NextPage => CurrentPage < TotalPages ? CurrentPage + 1 : CurrentPage;
    public int PreviousPage => CurrentPage > 1 ? CurrentPage - 1 : CurrentPage;
    public int PageCount => Data.Count;
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
    public bool HasNextPage => CurrentPage < TotalPages && TotalPages > 0;
    public bool HasPreviousPage => CurrentPage > 1 && TotalPages > 0;

    public PagedResult()
    {
        Data = new List<T>();
    }
    
    public PagedResult(int currentPage, int pageSize, int totalCount, IList<T> data)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
        Data = data ?? new List<T>();
    }

    public static PagedResult<T> Empty() => new PagedResult<T>(1, 15, 0, new List<T>());
}
