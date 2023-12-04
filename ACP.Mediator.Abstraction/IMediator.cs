namespace ACP.Mediator.Abstraction;

public interface IMediator
{
    Task SendAsync<T>(T request, CancellationToken cancelToken = default) where T : class, IRequest;

    Task<TRes> SendAsync<TRes>(IRequest<TRes> request, CancellationToken cancelToken = default);
}