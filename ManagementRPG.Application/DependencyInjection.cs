using ManagementRPG.Application.Global.Campanhas.Profiles;
using ManagementRPG.Application.Security.System.Profiles;
using ManagementRPG.Application.Security.Usuarios.Profiles;
using Microsoft.Extensions.DependencyInjection;
using V4MAutoMapper;

namespace ManagementRPG.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            //AutoMapper
            services.AddAutoMapper();

            return services;
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection services) 
        {
            services.AddV4MAutoMapper(
                typeof(SistemaProfile).Assembly,
                typeof(CampanhaProfile).Assembly,
                typeof(UsuarioProfile).Assembly,
                typeof(UsuarioAuthLogProfile).Assembly
            );

            return services;
        }
    }
}