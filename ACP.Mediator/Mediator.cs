using Microsoft.Extensions.DependencyInjection;
using ACP.Mediator.Abstraction;

namespace ACP.Mediator;

public class Mediator : IMediator
{
    private readonly IServiceProvider _mServiceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        this._mServiceProvider = serviceProvider;
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancelToken)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

        var handler = this._mServiceProvider.GetRequiredService(handlerType);

        var methodInfo = handlerType.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.HandleAsync));

        return await (Task<TResponse>)methodInfo!.Invoke(handler, new object[] { request, cancelToken })!;
    }

    public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancelToken = default)
        where TRequest : class, IRequest
    {
        var handler = this._mServiceProvider.GetRequiredService<IRequestHandler<TRequest>>();
        await handler.HandleAsync(request, cancelToken);
    }
}