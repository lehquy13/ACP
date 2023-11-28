using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Mediator.Abstraction;

public interface IMediator
{
    Task SendAsync<T>(T request, CancellationToken cancelToken) where T : class, IRequest;

    Task<TRes> SendAsync<TRes>(IRequest<TRes> request, CancellationToken cancelToken);
}