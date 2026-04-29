using ManagementRPG.API.Authorization;
using ManagementRPG.Application;
using ManagementRPG.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi;

namespace ManagementRPG.API.Extensions
{
    internal static class ServiceExtensions
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            // Adicione o serviço CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:44306")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // Add services to the container.
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    //Description = "Insira o token JWT"
                });
            });

            //Configure Application
            services.AddApplication();

            //Configure Infrastrcuture
            services.AddInfrastructure(configuration);

            //Configure Authorization
            services.AddAuthorizationPolicies();

            return services;
        }
    }
}
