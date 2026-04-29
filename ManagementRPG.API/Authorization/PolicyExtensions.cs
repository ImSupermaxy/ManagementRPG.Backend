using ManagementRPG.Domain.Security.Usuarios.Enums;
using ManagementRPG.Domain.Shared.ApiConfig.Settings;

namespace ManagementRPG.API.Authorization
{
    public static class PolicyExtensions
    {
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.ADMIN, policy => policy.RequireRole(TokenAuthConfig.Role, 
                    EPerfil.ADMINISTRADOR.ToString()));

                options.AddPolicy(Policy.MASTER, policy => policy.RequireRole(TokenAuthConfig.Role, 
                    EPerfil.MASTER.ToString(), 
                    EPerfil.ADMINISTRADOR.ToString()));

                options.AddPolicy(Policy.COMMON, policy => policy.RequireRole(TokenAuthConfig.Role, 
                        EPerfil.MASTER.ToString(), 
                        EPerfil.ADMINISTRADOR.ToString(), 
                        EPerfil.USUARIO.ToString()));
            });

            return services;
        }
    }
}
