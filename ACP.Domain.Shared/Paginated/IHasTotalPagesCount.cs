namespace ACP.Domain.Shared.Paginated;

public interface IHasTotalPagesCount
{
    public int TotalPages { get; }
}