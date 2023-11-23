namespace ACP.Domain.Shared.Paginated;

public interface IPaginated
{
    public int PageIndex { get; }
    public int PageSize { get; }
}

public interface IHasTotalItemsCount
{
    public int TotalItems { get; }
}public interface IHasTotalPagesCount
{
    public int TotalPages { get; }
}