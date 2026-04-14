using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi;

namespace ManagementRPG.API.Config
{
    internal sealed class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription apiVersionDescription)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = $"ManagementRPG.API V{apiVersionDescription.ApiVersion}",
                Version = apiVersionDescription.ApiVersion.ToString()
            };

            if (apiVersionDescription.IsDeprecated)
            {
                openApiInfo.Description += " This API version has been deprecated.";
            }

            return openApiInfo;
        }
    }

    //internal sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    //{
    //    private readonly IApiVersionDescriptionProvider _provider;

    //    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    //        => _provider = provider;

    //    public void Configure(SwaggerGenOptions options)
    //    {
    //        foreach (var description in _provider.ApiVersionDescriptions)
    //        {
    //            options.SwaggerDoc(
    //                description.GroupName,
    //                new OpenApiInfo()
    //                {
    //                    Title = "ManagementRPG API",
    //                    Version = description.ApiVersion.ToString()
    //                });
    //        }
    //    }
    //}
}