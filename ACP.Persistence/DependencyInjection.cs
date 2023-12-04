using ACP.Domain.Business;
using ACP.Domain.Business.Identities;
using ACP.Domain.Interfaces;
using ACP.Domain.Interfaces.Repositories;
using ACP.Infrastructure.Persistence.EntityFrameworkCore;
using ACP.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ACP.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            ConfigurationManager configuration
        )
        {
            // set configuration settings to emailSettingName and turn it into Singleton

            services.AddDbContext<AcpDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                )
            );

            //Seed data using DataSeed
            var dbContext = services
                .BuildServiceProvider()
                .GetRequiredService<AcpDbContext>();
            //DataSeed.Execute(dbContext).GetAwaiter();

            // Dependency Injection for repository
            //services.AddLazyCache();

            services.AddScoped(typeof(IRepository<,>), typeof(RepositoryImpl<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();

            return services;
        }
    }
}