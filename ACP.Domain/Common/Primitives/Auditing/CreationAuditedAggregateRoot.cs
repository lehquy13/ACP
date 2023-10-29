using ACP.Auditing.Abstractions;

namespace ACP.Domain.Common.Primitives.Auditing;

public abstract class CreationAuditedAggregateRoot<TId> : AggregateRoot<TId>, ICreationAuditedObject<TId> where TId : notnull
{
    public DateTime CreationTime { get; set; }

    public TId? CreatorId { get; set; }

    protected CreationAuditedAggregateRoot()
    {
    }

    protected CreationAuditedAggregateRoot(TId id)
        : base(id)
    {
    }
}