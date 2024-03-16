using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CleanFunctionApp.Test.IntegrationTest;

public class Repository
{
    protected DbContextOptionsBuilder<Context>? optionsBuilder;
    protected Context context;
    protected UnitOfWork unitOfWork;

    private string connString =
        "Server=tcp:localhost,1433;Initial Catalog=jobPortalV2Test;Persist Security Info=False;User ID=sa;Password=1986.Yusuf+;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Enlist=false";
    
    private static readonly object _lock = new();
    private static bool _databaseInitialized;
    

    public Repository()
    {
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
        
        unitOfWork = new UnitOfWork(context);
    }

    public Context CreateContext()
        => new(
            new DbContextOptionsBuilder<Context>()
                .UseSqlServer(connString)
                .Options);
}