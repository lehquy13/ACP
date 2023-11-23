using ACP.Domain.Entities;

namespace ACP.Domain.Specifications.Users;

public sealed class UserListQuerySpec : GetListSpecificationBase<User>
{
    public UserListQuerySpec(
        int pageIndex,
        int pageSize)
        : base(pageIndex, pageSize)
    {
    }
}