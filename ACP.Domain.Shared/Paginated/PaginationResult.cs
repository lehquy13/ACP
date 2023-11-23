using ACP.Results;

namespace ACP.Domain.Shared.Paginated;

public class PaginationResult<T> : ResultBase, IPaginated, IHasTotalItemsCount, IHasTotalPagesCount
    where T : notnull
{
    public int PageIndex { get; private set; }

    public int PageSize { get; private set; }

    public int TotalItems { get; private set; }

    public int TotalPages { get; private set; }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public List<T> Value { get; private set; }

    /// <summary>
    /// PaginationResult is automatically set to success with a value of empty list
    /// </summary>
    /// <param name="value"></param>
    /// <param name="totalCount"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    private PaginationResult(List<T> value, int totalCount, int pageIndex, int pageSize) : base(true, Error.None)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        TotalItems = totalCount;
        PageSize = pageSize;
        Value = value;
    }

    public static PaginationResult<T> Create(
        List<T> items,
        int totalCount,
        int pageIndex,
        int pageSize,
        string message = "")
    {
        return new PaginationResult<T>(items, totalCount, pageIndex, pageSize);
    }

    public static implicit operator PaginationResult<T>(Result result)
        => new(new(), 0, 0, 0);
}