using Domain.Aggregation.JobPost;

namespace Domain.Abstract;

public interface IUnitOfWork
{
    ITransaction BeginTransaction();
    Task CommitAsync(CancellationToken cancellationToken);
    void Detach(object entity);
    IJobPostService JobPostService();
}