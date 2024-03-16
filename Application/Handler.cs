namespace Application;

public abstract class Handler(IUnitOfWork unitOfWork)
{
    protected readonly IUnitOfWork UnitOfWork = unitOfWork;

    protected static Task<T> Success<T>(T dto)  => Task.FromResult(dto);
    protected static Task Success() => Task.CompletedTask;
}