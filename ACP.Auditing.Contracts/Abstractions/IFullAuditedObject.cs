namespace ACP.Auditing.Abstractions;

/// <summary>
/// Adds user navigation properties to <see cref="IFullAuditedObject"/> interface for user.
/// </summary>
/// <typeparam name="TId">Type of the user</typeparam>
public interface IFullAuditedObject<TId> : IAuditedObject<TId>, IDeletionAuditedObject<TId>, IHasCreationTime
{
}