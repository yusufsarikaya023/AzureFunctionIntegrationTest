namespace Domain.Aggregation.Common;

public class Sucess() : Response(true);

public class Success<T>(T body, int size = 0)
{
    public T Body { get; set; } = body;
    public int Size { get; set; } = size;
}