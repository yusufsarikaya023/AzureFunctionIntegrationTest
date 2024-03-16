using Domain.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Injector
{
    public static void RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}