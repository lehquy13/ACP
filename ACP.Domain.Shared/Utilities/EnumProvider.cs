using ACP.Domain.Shared.User;

namespace ACP.Domain.Shared.Utilities;

public static class EnumProvider
{
    public static List<string> Genders { get; } = Enum.GetNames(typeof(Gender)).ToList();
}