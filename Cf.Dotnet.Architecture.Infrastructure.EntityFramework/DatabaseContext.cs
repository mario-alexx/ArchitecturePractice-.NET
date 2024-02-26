using System.Data;
using Cf.Dotnet.Architecture.Domain.Entities;
using Cf.Dotnet.Architecture.Domain.SeedWork;
using Cf.Dotnet.Architecture.Infrastructure.Abstractions;
using Cf.Dotnet.Database.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cf.Dotnet.Database;

/// <summary>
///     Contexto de base de datos para la aplicación, utilizado para interactuar con la base de datos.
///     Hereda de DbContext, una clase de Entity Framework Core que facilita el mapeo entre objetos y registros de base de
///     datos.
/// </summary>
public class DatabaseContext : DbContext, IUnitOfWork, IDatabaseContext
{
    private IDbContextTransaction? currentTransaction;

    /// <summary>
    ///     Constructor sin parámetros para el contexto de base de datos.
    ///     Utilizado por herramientas de Entity Framework Core.
    /// </summary>
    public DatabaseContext()
    {
    }

    /// <summary>
    ///     Constructor que permite la configuración de opciones para el contexto de base de datos.
    /// </summary>
    /// <param name="options">Opciones de configuración para el contexto.</param>
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Buyer> Buyers { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    public bool HasActiveTransaction => currentTransaction != null;

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        return currentTransaction;
    }

    public async Task CommitTransactionAsync()
    {
        if (currentTransaction is null) throw new InvalidOperationException("No active transaction");

        try
        {
            await SaveChangesAsync();
            await currentTransaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            currentTransaction.Dispose();
            currentTransaction = null;
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            currentTransaction?.Rollback();
        }
        finally
        {
            if (currentTransaction is not null)
            {
                currentTransaction.Dispose();
                currentTransaction = null;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderItemModelConfiguration());
        modelBuilder.ApplyConfiguration(new OrderModelConfiguration());
        modelBuilder.ApplyConfiguration(new BuyerModelConfiguration());
    }
}