using Asp.Versioning.ApiExplorer;
using ManagementRPG.API.Extensions;
using ManagementRPG.Domain.Shared.ApiConfig;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Configure Services
builder.Services.ConfigureService(builder.Configuration);

//Build app
var app = builder.Build();

//Apply the RunMode to Project
RunMode.SetMode(app.Environment.IsDevelopment(), app.Environment.IsStaging(), app.Environment.IsProduction());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapScalarApiReference(options =>
    {
        options.AddPreferredSecuritySchemes("Bearer");

        foreach (ApiVersionDescription description in app.DescribeApiVersions())
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            options.OpenApiRoutePattern = url;
        }

        options.Title = "Management API";
        options.Theme = ScalarTheme.DeepSpace;
        options.ForceThemeMode = ThemeMode.Dark;
    });

    //app.ApplyMigrations();

    // REMARK: Uncomment if you want to seed initial data.
    //app.SeedData();
}

//Configure ApplicationBuilder
app.ConfigureAppBuilder();

app.MapControllers();

//Run app
app.Run();
