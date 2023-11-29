using ACP.Application.Contracts.Interfaces.Business;
using ACP.Application.Mapping;
using ACP.Application.ServiceImpls;
using ACP.Domain.DomainServices;
using ACP.Domain.DomainServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ACP.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddApplicationMappings();
            
            //Domain services
            services.AddTransient<IIdentityDomainServices, IdentityDomainServices>();

            //Application services
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<IUserServices, UserServices>();

            return services;
        }
    }
}