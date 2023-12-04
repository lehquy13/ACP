using ACP.Application.Contracts.DataTransferObjects.Authentications;
using ACP.Mediator.Abstraction;

namespace ACP.Application.ServiceImpls;

internal class TestQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    public async Task<AuthenticationResult> HandleAsync(LoginQuery request, CancellationToken cancelToken = default)
    {
        await Task.CompletedTask;

        return new AuthenticationResult(new(), "Token generated");
    }
}

internal class TestQueryHandler1 : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    public async Task<AuthenticationResult> HandleAsync(RegisterCommand request, CancellationToken cancelToken = default)
    {
        await Task.CompletedTask;

        return new AuthenticationResult(new(), "Register Token generated");
    }
}