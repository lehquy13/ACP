using ACP.Application.Contracts.DataTransferObjects.Authentications;
using ACP.Application.Contracts.Interfaces.Business;
using ACP.Application.Mapping;
using ACP.Application.ServiceImpls;
using ACP.Domain.DomainServices;
using ACP.Domain.DomainServices.Interfaces;
using ACP.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace ACP.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddApplicationMappings();

            //Other services
            services.AddMediator(typeof(DependencyInjection).Assembly, typeof(LoginQuery).Assembly);

            //Domain services
            services.AddTransient<IIdentityDomainServices, IdentityDomainServices>();

            //Application services
            return services;
        }
    }
}