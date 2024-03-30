using Application;
using Infrastructure;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Test;

public class Startup(string connString) : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddDbContext<Context>(options =>
        {
            options.UseSqlServer(connString)
                .UseSnakeCaseNamingConvention();
        });

        builder.Services.RegisterApplication();
        builder.Services.RegisterInfrastructure();
        builder.Services.AddHttpContextAccessor();
    }
}