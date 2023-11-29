namespace ACP.Domain.Shared.Paginated;

public interface IHasTotalItemsCount
{
    public long TotalItems { get; }
}