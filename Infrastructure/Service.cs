using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Service<T> where T: class
{
    protected readonly Context Context;
    protected readonly DbSet<T> DbSet;

    protected Service(Context context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }
}