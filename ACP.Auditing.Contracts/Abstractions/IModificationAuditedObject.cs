namespace ACP.Auditing.Abstractions;

/// <summary>
/// This interface can be implemented to store modification information (who and when modified lastly).
/// </summary>
public interface IModificationAuditedObject<TLastModifierId> : IHasModificationTime
{
    /// <summary>
    /// Last modifier user for this entity.
    /// </summary>
    TLastModifierId? LastModifierId { get; }
}