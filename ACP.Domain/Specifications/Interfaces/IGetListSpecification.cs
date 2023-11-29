using ACP.Domain.Shared.Paginated;

namespace ACP.Domain.Specifications.Interfaces;

public interface IGetListSpecification<TLEntity> : ISpecification<TLEntity>, IPaginated;