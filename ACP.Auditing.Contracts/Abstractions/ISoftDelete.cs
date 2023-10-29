namespace ACP.Auditing.Abstractions;

public interface ISoftDelete
{
    // Summary:
    //     Used to mark an Entity as 'Deleted'.
    bool IsDeleted { get; set; }
}