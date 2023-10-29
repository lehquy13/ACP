namespace ACP.Auditing.Abstractions;

/// <summary>
/// This interface can be implemented to store deletion information (who delete and when deleted).
/// </summary>
public interface IDeletionAuditedObject<TDeleterId> : IHasDeletionTime
{
    /// <summary>
    /// Id of the deleter user.
    /// </summary>
    TDeleterId? DeleterId { get; }
}
