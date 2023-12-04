namespace ACP.Mediator.Abstraction;

public interface IMediatorRequestHandler;

public interface IRequestHandler<in TReq> : IMediatorRequestHandler where TReq : class, IRequest
{
    Task HandleAsync(TReq request, CancellationToken cancelToken = default);
}

public interface IRequestHandler<in TReq, TRes> : IMediatorRequestHandler where TReq : class, IRequest<TRes>
{
    Task<TRes> HandleAsync(TReq request, CancellationToken cancelToken = default);
}