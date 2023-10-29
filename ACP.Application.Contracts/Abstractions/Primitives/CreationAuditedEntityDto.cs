using ACP.Auditing.Abstractions;

namespace ACP.Application.Contracts.Abstractions.Primitives;

public abstract class CreationAuditedEntityDto<TId> : EntityDto<TId>, ICreationAuditedObject<TId> where TId : notnull
{
    public DateTime CreationTime { get; init; }
    
    public TId? CreatorId { get; set; }
    
    protected CreationAuditedEntityDto(TId id) : base(id)
    {
        CreationTime = DateTime.Now.ToLocalTime();
    }

    protected CreationAuditedEntityDto()
    {
        CreationTime = DateTime.Now.ToLocalTime();
    }
}