using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Test.IntegrationTest;

public class FunctionApplicationStartup
{
    public readonly IHost host;

    private string connString =
        "Server=tcp:localhost,1433;Initial Catalog=jobPortalV2Test;Persist Security Info=False;User ID=sa;Password=1986.Yusuf+;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Enlist=false";

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    protected DbContextOptionsBuilder<Context>? optionsBuilder;
    protected Context context;
    protected UnitOfWork unitOfWork;

    public FunctionApplicationStartup()
    {
        var startup = new Startup();
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
                .UseSqlServer(connString)
                .Options);
}