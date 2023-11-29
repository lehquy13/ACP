namespace ACP.Domain.Shared.Paginated;

public interface IPaginated
{
    public int PageIndex { get; }
    public int PageSize { get; }
}