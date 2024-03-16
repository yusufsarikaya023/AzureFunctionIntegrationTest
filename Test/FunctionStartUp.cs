using Application;
using Infrastructure;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Test;

public class Startup : FunctionsStartup
{
    private string connString =
        "Server=tcp:localhost,1433;Initial Catalog=jobPortalV2Test;Persist Security Info=False;User ID=sa;Password=1986.Yusuf+;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Enlist=false";

    public override void Configure(IFunctionsHostBuilder builder)
    {
        // if mediatr is already registered, don't register it again
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
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