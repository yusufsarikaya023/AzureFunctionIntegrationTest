using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public class Dependency
{
    private static IServiceProvider? provider;

    /// <summary>
    /// Build IoC container
    /// </summary>
    /// <param name="provider"></param>
    public static void Build(IServiceProvider provider)
    {
        Dependency.provider ??= provider;
    }

    /// <summary>
    /// Resolve dependency
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static T Get<T>() where T : notnull
    {
        using var scope = provider!.CreateScope();
        return scope.ServiceProvider.GetRequiredService<T>();
    }
}