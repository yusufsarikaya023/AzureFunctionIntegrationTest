using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Testcontainers.MsSql;

namespace Test.IntegrationTest;

public class FunctionApplicationStartup: IAsyncLifetime
{
    public readonly IHost host;

    public MsSqlContainer container = new MsSqlBuilder()
        .WithName("TestDb")
        .WithPassword("Password1234!")
        .Build();
    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public FunctionApplicationStartup()
    {
        InitializeAsync().Wait();
        // set default database to TestDb
        var connectionString = container.GetConnectionString().Replace("master", "TestDb");
        var startup = new Startup(connectionString);
        host = new HostBuilder().ConfigureWebJobs(startup.Configure).Build();
        host.Start();

        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    context.SaveChanges();
                }

                _databaseInitialized = true;
            }
        }
    }

    public Context CreateContext()
        => new(
            new DbContextOptionsBuilder<Context>()
                .UseSqlServer(container.GetConnectionString().Replace("master", "TestDb"))
                .Options);

    public Task InitializeAsync()
    {
        return container.StartAsync();
    }

    public Task DisposeAsync()
    {
        return container.DisposeAsync().AsTask();
    }
}