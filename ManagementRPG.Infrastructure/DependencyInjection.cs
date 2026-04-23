using Asp.Versioning;
using ManagementRPG.Application.Abstractions.Clock;
using ManagementRPG.Application.Global.Campanhas.Handlers;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Global.Campanhas.Repositories;
using ManagementRPG.Domain.Security.System.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using ManagementRPG.Domain.Shared.ApiConfig.Authentication;
using ManagementRPG.Infrastructure.Authentication;
using ManagementRPG.Infrastructure.Clock;
using ManagementRPG.Infrastructure.Context;
using ManagementRPG.Infrastructure.Context.Postgres;
using ManagementRPG.Infrastructure.Global.Campanhas.Repositories;
using ManagementRPG.Infrastructure.Providers;
using ManagementRPG.Infrastructure.Security.System.Repositories;
using ManagementRPG.Infrastructure.Security.Usuarios.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;

namespace ManagementRPG.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            AddPersistence(services, configuration);

            //AddCaching(services, configuration);

            AddAuthentication(services, configuration);

            AddAuthorization(services);

            AddHealthChecks(services, configuration);

            AddApiVersioning(services);

            //AddBackgroundJobs(services, configuration);

            return services;
        }

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Database") ??
                                      throw new ArgumentNullException(nameof(configuration));

            //Context
            services.AddScoped<IDBContext>(sp => new DBContextPostgres(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Security - Entitys
            services.AddScoped<ISistemaRepository, SistemaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            //services.AddScoped<CampanhaHandler, CampanhaHandler>();

            //Global - Entitys
            services.AddScoped<ICampanhaRepository, CampanhaRepository>();
            services.AddScoped<CampanhaHandler, CampanhaHandler>();

            //SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        }

        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            //AppSettings
            services.AddSingleton<IAppAuthSettings>(configuration.GetSection("GeneralSettings").Get<AppAuthSettings>()!);

            // Obtém a seção "AppSettings" do arquivo de configuração
            var appSettingsSection = configuration.GetSection("GeneralSettings");

            // Configura a classe AppSettings para ser injetada via DI
            services.Configure<AppAuthSettings>(appSettingsSection);

            // Obtém a instância de AppSettings
            var appSettings = appSettingsSection.Get<AppAuthSettings>();

            // Converte a chave secreta para um array de bytes
            var key = Encoding.ASCII.GetBytes(appSettings?.Secret ?? "");

            // Configura o serviço de autenticação JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Usa a chave secreta do AppSettings
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        }

        private static void AddAuthorization(IServiceCollection services)
        {
        }

        private static void AddCaching(IServiceCollection services, IConfiguration configuration)
        {
            //string connectionString = configuration.GetConnectionString("Cache") ??
            //                          throw new ArgumentNullException(nameof(configuration));

            //services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

            //services.AddSingleton<ICacheService, CacheService>();
        }

        private static void AddHealthChecks(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();
                //.AddNpgSql(configuration.GetConnectionString("Database")!)
                //.AddRedis(configuration.GetConnectionString("Cache")!);
        }

        private static void AddApiVersioning(IServiceCollection services)
        {
            services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1);
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddMvc()
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
        }

        private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigProviders>(configuration.GetSection("Providers"));

            services.AddQuartz();

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            //services.ConfigureOptions<MyServiceProviderWithJobToExecute>();
        }
    }
}
