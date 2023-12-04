using ACP.DependencyInjection;

namespace ACP.Mediator.Abstraction;

public interface IBaseRequest;

public interface IRequest : IBaseRequest;

public interface IRequest<T> : IBaseRequest;