namespace Domain.Aggregation.Common;

public class Response(bool success = true)
{
    public bool Success { get; set; } = success;
}