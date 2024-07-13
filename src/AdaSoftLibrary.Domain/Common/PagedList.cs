using Microsoft.EntityFrameworkCore;

namespace AdaSoftLibrary.Domain.Common;

public class PagedList<T>
{
    public PagedList(IReadOnlyCollection<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    public IReadOnlyCollection<T> Items { get; private set; } = new List<T>();

    public int PageNumber { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    public int TotalPages { get; }

    public bool HasPreviousPage => (PageNumber > 1);

    public bool HasNextPage => (PageNumber < TotalPages);

    public static PagedList<T> Create(
        IQueryable<T> source,
        int pageIndex,
        int pageSize)
    {
        var totalCount = source.Count();

        var items = source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedList<T>(items, pageIndex, pageSize, totalCount);
    }

    public static async Task<PagedList<T>> CreateAsync(
        IQueryable<T> source,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<T>(items, pageIndex, pageSize, totalCount);
    }
}
