using ACP.Auditing.Abstractions;

namespace ACP.Domain.Common.Primitives.Auditing;

public abstract class FullAuditedAggregateRoot<TId> : AuditedAggregateRoot<TId>, IFullAuditedObject<TId>
    where TId : notnull
{
    public bool IsDeleted { get; set; }

    public DateTime? DeletionTime { get; set; }

    public TId? DeleterId { get; set; }

    protected FullAuditedAggregateRoot(TId id) : base(id)
    {
        CreationTime = DateTime.Now.ToLocalTime();
    }

    protected FullAuditedAggregateRoot()
    {
    }
}