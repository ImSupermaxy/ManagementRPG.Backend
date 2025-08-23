using FluentValidation;
using ManagementRPG.Application.Commands.Campanhas;
using ManagementRPG.Domain.Commands.Campanhas;
using Microsoft.Extensions.DependencyInjection;

namespace ManagementRPG.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                //configuration.RegisterServicesFromAssembly(typeof(CampanhaCommandGetAll).Assembly);
                //configuration.RegisterServicesFromAssembly(typeof(CampanhaCommandGetById).Assembly);
                //configuration.RegisterServicesFromAssembly(typeof(CampanhaCommandRemove).Assembly);
                //configuration.RegisterServicesFromAssembly(typeof(CampanhaCommandInsert).Assembly);
                //configuration.RegisterServicesFromAssembly(typeof(CampanhaCommandUpdate).Assembly);

                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                //configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
                //configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
                //configuration.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
            });

            //services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

            return services;
        }
    }
}