using ACP.Auditing.Abstractions;

namespace ACP.Domain.Common.Primitives.Auditing;

public abstract class AuditedAggregateRoot<TId> : AggregateRoot<TId>, IAuditedObject<TId>
    where TId : notnull
{
    //Creation
    public DateTime CreationTime { get; init; }

    public TId? CreatorId { get; set; }

    //Modification
    public DateTime? LastModificationTime { get; }

    public TId? LastModifierId { get; set; }

    protected AuditedAggregateRoot(TId id) : base(id)
    {
        CreationTime = DateTime.Now.ToLocalTime();
        LastModificationTime = DateTime.Now.ToLocalTime();
    }

    protected AuditedAggregateRoot()
    {
    }
}