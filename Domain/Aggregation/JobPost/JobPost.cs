using Domain.Aggregation.Common;

namespace Domain.Aggregation.JobPost;

public class JobPost: Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}