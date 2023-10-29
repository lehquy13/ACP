using ACP.Auditing.Abstractions;

namespace ACP.Domain.Common.Primitives.Auditing;

public abstract class AuditedEntity<TId> : CreationAuditedEntity<TId>, IAuditedObject<TId>
    where TId : notnull
{
    //Modification
    public DateTime? LastModificationTime { get; }

    public TId? LastModifierId { get; set; }

    protected AuditedEntity(TId id) : base(id)
    {
        LastModificationTime = DateTime.Now.ToLocalTime();
    }

    protected AuditedEntity()
    {
    }
}