using ACP.Auditing.Abstractions;

namespace ACP.Domain.Common.Primitives.Auditing;

public abstract class FullAuditedEntity<TId> : AuditedEntity<TId>, IDeletionAuditedObject<TId>
    where TId : notnull
{
    public DateTime? DeletionTime { get; set; }

    public bool IsDeleted { get; set; }

    public TId? DeleterId { get; set; }

    protected FullAuditedEntity(TId id) : base(id)
    {
    }

    protected FullAuditedEntity()
    {
    }
}