using ACP.Application.Contracts.DataTransferObjects.Authentications;
using ACP.Domain.Business;
using Mapster;

namespace ACP.Application.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, IdentityUserDto>()
            .Map(des => des, src => src); 
    }
}