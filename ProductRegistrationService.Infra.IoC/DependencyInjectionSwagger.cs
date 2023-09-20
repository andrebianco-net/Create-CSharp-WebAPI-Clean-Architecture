using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProductRegistrationService.Infra.IoC
{
    public static class DependencyInjectionSwagger
    {
        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services, IConfiguration configuration)
        {

            // Configurations and give support JWT            
            services.AddSwaggerGen(options => {                

                options.SwaggerDoc(configuration["Swagger:Name"], new OpenApiInfo { 
                    Title = configuration["Swagger:Title"],
                    Version = configuration["Swagger:Version"],
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] " + 
                                  "and then your token in the text input below. \r\n\r\nExample: \"Bearer 12345abcdef\"", 
                });
                
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

            });
            
            return services;

        }
    }
}