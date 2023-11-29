using ACP.Domain.Specifications.Interfaces;

namespace ACP.Domain.Specifications;

public abstract class GetListSpecificationBase<TEntity>
    : SpecificationBase<TEntity>, IGetListSpecification<TEntity>
{
    public int PageIndex { get; private set; }

    public int PageSize { get; private set; }

    public GetListSpecificationBase(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        ApplyPaging((pageIndex - 1) * pageSize, pageSize);
    }
}