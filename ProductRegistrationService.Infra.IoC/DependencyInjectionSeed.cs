using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductRegistrationService.Domain.Account;
using ProductRegistrationService.Infra.Data.Identity;

namespace ProductRegistrationService.Infra.IoC
{
    public static class DependencyInjectionSeed
    {
        public static IServiceCollection AddInfrastructureSeed(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var seed = scope.ServiceProvider.GetRequiredService<ISeedUserRoleInitial>();

                //It will create new roles and users if they do not exists yet.
                //No duplicity will be generated.
                seed.SeedRoles();
                seed.SeedUsers();
            }

            return services;
        }
    }
}