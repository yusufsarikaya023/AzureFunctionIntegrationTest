using Domain.Abstract;
using Domain.Aggregation.JobPost;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UnitOfWork(Context context) : IUnitOfWork
{
    public ITransaction BeginTransaction() => new Transaction(context);

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return context.SaveChangesAsync(cancellationToken);
    }

    public void Detach(object entity)
    {
        context.Entry(entity).State = EntityState.Detached;
    }

    private IJobPostService? _jobPostService;
    public IJobPostService JobPostService() => _jobPostService ??= new JobPostService(context);

}