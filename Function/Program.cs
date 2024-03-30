using Application;
using Infrastructure;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureOpenApi()
    .ConfigureAppConfiguration(c =>
    {
        c.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices(s =>
    {
        var connectionString = s.BuildServiceProvider()
            .GetRequiredService<IConfiguration>()["ConnectionStrings:DefaultConnection"];

        s.RegisterApplication();
        s.RegisterInfrastructure();
        s.AddHttpContextAccessor();
        s.AddDbContext<Context>(options => options.UseSqlServer(connectionString).UseSnakeCaseNamingConvention());
        
        s.AddSingleton<IOpenApiConfigurationOptions>(_ =>
        {
            var options = new OpenApiConfigurationOptions
            {
                Info = new OpenApiInfo
                {
                    Version = "2.0",
                    Title = "Serverless Job Portal API",
                    Description = "This is the API on which the serverless job portal engine is running.",
                    TermsOfService = new Uri("https://www.jobportal.se"),
                    Contact = new OpenApiContact
                    {
                        Name = "Jobportal",
                        Email = "info@jobportal.se",
                        Url = new Uri("https://www.jobportal.se")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License",
                        Url = new Uri("https://www.jobportal.se")
                    }
                },
                OpenApiVersion = OpenApiVersionType.V3,
                IncludeRequestingHostName = true,
                ForceHttps = false,
                ForceHttp = false,
            };
            return options;
        });

    })
    .Build();
await host.RunAsync();