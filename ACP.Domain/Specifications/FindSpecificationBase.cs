using ACP.Domain.Specifications.Interfaces;

namespace ACP.Domain.Specifications;

public abstract class FindSpecificationBase<TEntity, TId>(TId id) 
    : SpecificationBase<TEntity>,
    IFindSpecification<TEntity, TId>
{
    public TId Id { get; private set; } = id;
}