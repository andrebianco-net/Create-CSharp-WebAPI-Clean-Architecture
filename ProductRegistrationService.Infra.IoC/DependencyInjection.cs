using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductRegistrationService.Application.interfaces;
using ProductRegistrationService.Application.Mappings;
using ProductRegistrationService.Application.Services;
using ProductRegistrationService.Context;
using ProductRegistrationService.Domain.Account;
using ProductRegistrationService.Domain.interfaces;
using ProductRegistrationService.Infra.Data.Identity;
using ProductRegistrationService.Infra.Data.Repositories;

namespace ProductRegistrationService.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                options.UseSqlServer(
                            configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                        )
            );

            // Authenticate
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IAuthenticate, AuthenticateService>();

            services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Account/Login");

            // Repository
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Service
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            // Mapper
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}