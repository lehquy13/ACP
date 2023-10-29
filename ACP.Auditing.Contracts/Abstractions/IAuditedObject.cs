namespace ACP.Auditing.Abstractions;

/// <summary>
/// This interface can be implemented to add standard auditing properties to a class.
/// </summary>
/// <typeparam name="TId">Type of the user</typeparam>
public interface IAuditedObject<TId> : ICreationAuditedObject<TId>, IModificationAuditedObject<TId>
{
}
