namespace ACP.Application.Contracts.DataTransferObjects.Users;

public record UserForUpsertDto(Guid Id, string Name, string City, string Country);