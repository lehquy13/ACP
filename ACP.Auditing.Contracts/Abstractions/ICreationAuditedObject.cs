namespace ACP.Auditing.Abstractions;

/// <summary>
/// This interface can be implemented to store creation information (who and when created).
/// </summary>
/// <typeparam name="TCreatorId">Type of the user</typeparam>
public interface ICreationAuditedObject<TCreatorId> 
{
    TCreatorId? CreatorId { get; }
}
