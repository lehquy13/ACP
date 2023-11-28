using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Mediator.Abstraction;

public interface IRequestHandler<in TReq> where TReq : class, IRequest
{
    Task HandleAsync(TReq request, CancellationToken cancelToken = default);
}

public interface IRequestHandler<in TReq, TRes> where TReq : class, IRequest<TRes>
{
    Task<TRes> HandleAsync(TReq request, CancellationToken cancelToken = default);
}
