using ACP.Auditing.Abstractions;

namespace ACP.Application.Contracts.Abstractions.Primitives;

public abstract class FullAuditedEntityDto<TId> : AuditedEntityDto<TId>, IFullAuditedObject<TId>
    where TId : notnull
{
    //Deletions
    public DateTime? DeletionTime { get; set; }

    public TId? DeleterId { get; set; }

    public bool IsDeleted { get; set; }

    protected FullAuditedEntityDto(TId id) : base(id)
    {
    }

    protected FullAuditedEntityDto()
    {
    }
}