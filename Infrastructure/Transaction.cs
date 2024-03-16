using Domain.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

public class Transaction(DbContext context) : ITransaction
{
    private readonly IDbContextTransaction _scope = context.Database.BeginTransaction();

    public Task CommitAsync(CancellationToken cancellationToken) => _scope.CommitAsync(cancellationToken);
    public Task RollbackAsync(CancellationToken cancellationToken) => _scope.RollbackAsync(cancellationToken);
    public void Dispose() => _scope.Dispose();
}