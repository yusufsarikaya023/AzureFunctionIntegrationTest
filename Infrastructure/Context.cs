using Domain.Aggregation.JobPost;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public Context(DbContextOptionsBuilder<Context> optionsBuilder) : base(optionsBuilder.Options)
    {
    }

    public DbSet<JobPost>? JobPosts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        // ChangeTracker.DetectChanges();
        // ModelWatcherSubject subject = new();
        // subject.AddObserver(new InsertObserver())
        //     .AddObserver(new UpdateObserver())
        //     .AddObserver(new DeleteObserver())
        //     .Publish(ChangeTracker.Entries());

        return base.SaveChangesAsync(cancellationToken);
    }
}