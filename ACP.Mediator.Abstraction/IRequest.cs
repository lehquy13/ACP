namespace ACP.Mediator.Abstraction;

public interface IBaseRequest { }

public interface IRequest : IBaseRequest { }

public interface IRequest<in T> : IBaseRequest { }