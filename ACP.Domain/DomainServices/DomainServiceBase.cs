using ACP.Domain.Interfaces;

namespace ACP.Domain.DomainServices;

public abstract class DomainServiceBase(IAppLogger<IdentityDomainServices> logger, IUnitOfWork unitOfWork)
{
    protected readonly IAppLogger<IdentityDomainServices> Logger = logger;
    protected readonly IUnitOfWork UnitOfWork = unitOfWork;
}