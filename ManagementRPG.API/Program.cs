using Asp.Versioning.ApiExplorer;
using ManagementRPG.API.Config;
using ManagementRPG.Application;
using ManagementRPG.Domain.Shared.ApiConfig;
using ManagementRPG.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicione o serviço CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:44306")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

//builder.Host.UseSerilog((context, loggerConfig) =>
//loggerConfig.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();
RunMode.SetMode(app.Environment.IsDevelopment(), app.Environment.IsStaging(), app.Environment.IsProduction());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (ApiVersionDescription description in app.DescribeApiVersions())
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();
            //options.SwaggerEndpoint(url, name);
        }

        //var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        //foreach (var description in provider.ApiVersionDescriptions)
        //{
        //    string url = $"/swagger/{description.GroupName}/swagger.json";
        //    string name = description.GroupName.ToUpperInvariant();
        //    //options.SwaggerEndpoint(url, name);
        //}
    });

    //builder.Services.AddSwaggerGen(options =>
    //{
    //    options.SwaggerDoc("v1", new OpenApiInfo
    //    {
    //        Version = "v1",
    //        Title = "ANSYS API",
    //        Description = "Uma ASP.NET Core Web API para gerenciamento de funcionarios, pedidos, menu e clientes de um restaurante.",
    //        //TermsOfService = new Uri("https://example.com/terms"),
    //        Contact = new OpenApiContact
    //        {
    //            Name = "Example Contact",
    //            Url = new Uri("https://example.com/contact")
    //        },
    //        License = new OpenApiLicense
    //        {
    //            Name = "Example License",
    //            Url = new Uri("https://example.com/license")
    //        }
    //    });
    //});

    //app.ApplyMigrations();

    // REMARK: Uncomment if you want to seed initial data.
    //app.SeedData();
}

//app.UseRequestContextLogging();

//app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.MapHealthChecks("health", new HealthCheckOptions
//{
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//});

app.Run();
