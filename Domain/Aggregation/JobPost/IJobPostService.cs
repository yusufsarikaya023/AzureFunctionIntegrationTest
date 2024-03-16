namespace Domain.Aggregation.JobPost;

public interface IJobPostService
{
    Task Insert(JobPost entity);
}