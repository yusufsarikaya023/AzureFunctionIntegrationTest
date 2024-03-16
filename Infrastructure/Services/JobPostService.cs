using Domain.Aggregation.JobPost;

namespace Infrastructure.Services;

public class JobPostService(Context context): Service<JobPost>(context), IJobPostService
{
    public Task Insert(JobPost entity)
    {
        DbSet.AddAsync(entity);
        return Task.CompletedTask;
    }
}