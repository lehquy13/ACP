using ACP.Domain.Interfaces;
using MapsterMapper;

namespace ACP.Application.ServiceImpls;

public abstract class ServiceBase(IMapper mapper,IUnitOfWork unitOfWork, IAppLogger<ServiceBase> logger)
{
    protected readonly IMapper Mapper = mapper;
    protected readonly IUnitOfWork UnitOfWork = unitOfWork;
    protected readonly IAppLogger<ServiceBase> Logger = logger;
}