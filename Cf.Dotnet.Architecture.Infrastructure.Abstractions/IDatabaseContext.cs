using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cf.Dotnet.Architecture.Infrastructure.Abstractions;

public interface IDatabaseContext
{
    public DatabaseFacade Database { get; }

    public bool HasActiveTransaction { get; }

    public Task<IDbContextTransaction> BeginTransactionAsync();

    public Task CommitTransactionAsync();

    public void RollbackTransaction();
}