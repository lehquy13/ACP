using System.Reflection;
using ACP.Mediator.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace ACP.Mediator
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMediator(
            this IServiceCollection services,
            params Assembly[] assembly)
        {
            services.AddScoped<IMediator, Mediator>();

            var requestTypes =
                assembly.SelectMany(x => x.GetTypes())
                    .Where(t => typeof(IBaseRequest).IsAssignableFrom(t))
                    .ToList();

            var handlerTypes =
                assembly.SelectMany(x => x.GetTypes())
                    .Where(t => typeof(IMediatorRequestHandler).IsAssignableFrom(t))
                    .ToList();

            for (int i = 0; i < requestTypes.Count; i++)
            {
                var (serviceType, implementationType) = GetTypes(handlerTypes[i]);

                RegisterWithTypes(services, serviceType, implementationType, ServiceLifetime.Scoped);
            }

            return services;
        }

        private static (Type serviceType, Type implementationType) GetTypes(Type serviceToRegister)
        {
            var genericInterface = serviceToRegister
                .GetInterfaces()
                .FirstOrDefault(x => x.IsGenericType && typeof(IMediatorRequestHandler).IsAssignableFrom(x));

            return (genericInterface != null
                    ? genericInterface
                    : serviceToRegister,
                serviceToRegister);
        }

        private static void RegisterWithTypes(IServiceCollection services, Type serviceType, Type implementationType,
            ServiceLifetime lifetime)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);

            services.Add(descriptor);
        }
    }
}