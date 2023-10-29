namespace ACP.Application.Contracts.Abstractions.Interfaces;

public interface IEntityDto<TId> 
{
    TId Id { get; set; }
}
