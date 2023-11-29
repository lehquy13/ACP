using ACP.Mediator.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Mediator;

public class Mediator : IMediator
{
    IServiceProvider m_serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        this.m_serviceProvider = serviceProvider;
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancelToken)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

        var handler = this.m_serviceProvider.GetRequiredService(handlerType);

        var methodInfo = handlerType.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.HandleAsync));

        return await (Task<TResponse>)methodInfo!.Invoke(handler, new object[] { request, cancelToken })!;
    }

    public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancelToken = default)
        where TRequest : class, IRequest
    {
        var handler = this.m_serviceProvider.GetRequiredService<IRequestHandler<TRequest>>();
        await handler.HandleAsync(request, cancelToken);
    }
}