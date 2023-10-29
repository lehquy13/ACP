using ACP.Auditing.Abstractions;

namespace ACP.Application.Contracts.Abstractions.Primitives;

public abstract class AuditedEntityDto<TId> : CreationAuditedEntityDto<TId>, IAuditedObject<TId> where TId : notnull
{
    //Modifications
    public DateTime? LastModificationTime { get; set; }

    public TId? LastModifierId { get; set; }

    protected AuditedEntityDto(TId id) : base(id)
    {
        LastModificationTime = DateTime.Now.ToLocalTime();
    }

    protected AuditedEntityDto()
    {
    }
}