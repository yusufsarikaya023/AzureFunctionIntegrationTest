using System.Reflection;
using Domain;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Injector
{
    public static void RegisterApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(typeof(Assembly).Assembly);
        services.AddMediatR(cfg => cfg.
            RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        Dependency.Build(services.BuildServiceProvider());
    }
}