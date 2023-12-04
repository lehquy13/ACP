using ACP.Domain.Interfaces;

namespace ACP.Domain.DomainServices;

public abstract class DomainServiceBase(IAppLogger<IdentityDomainServices> logger)
{
    protected readonly IAppLogger<IdentityDomainServices> Logger = logger;
}