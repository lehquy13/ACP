using ACP.Auditing.Abstractions;

namespace ACP.Domain.Common.Primitives.Auditing;

public abstract class CreationAuditedEntity<TId> : Entity<TId>, ICreationAuditedObject<TId> where TId : notnull
{
    public DateTime CreationTime { get; set; }

    public TId? CreatorId { get; set; }

    protected CreationAuditedEntity()
    {
        CreationTime = DateTime.Now.ToLocalTime();
    }

    protected CreationAuditedEntity(TId id)
        : base(id)
    {
    }
}