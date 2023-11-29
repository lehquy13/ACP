using ACP.Application.Contracts.Abstractions.Primitives;

namespace ACP.Application.Contracts.DataTransferObjects.Users;

public class UserForListDto : EntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"User with Id: {Id}, Name: {Name}";
    }
}