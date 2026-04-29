using ManagementRPG.Infrastructure;
using Asp.Versioning.ApiExplorer;
using ManagementRPG.API.Extensions;
using ManagementRPG.Domain.Shared.ApiConfig;
using Scalar.AspNetCore;

namespace ManagementRPG.API.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static void ConfigureAppBuilder(this IApplicationBuilder app)
        {


            //app.ApplyMigrations();

            //app.UseCustomExceptionHandler();

            //app.UseRequestContextLogging();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();
        }

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            //using IServiceScope scope = app.ApplicationServices.CreateScope();

            //using ExpensesMonitorContext dbContext = scope.ServiceProvider.GetRequiredService<ExpensesMonitorContext>();

            //dbContext.Database.Migrate();
        }

        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            //app.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
        {
            //app.UseMiddleware<RequestContextLoggingMiddleware>();

            return app;
        }
    }
}