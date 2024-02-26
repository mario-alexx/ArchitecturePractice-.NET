using Cf.Dotnet.Architecture.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cf.Dotnet.Architecture.Infrastructure.EntityFramework.MediatR.Behaviours;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IDatabaseContext dbContext;
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> logger;

    public TransactionBehavior(
        IDatabaseContext dbContext,
        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetType().Name;

        try
        {
            // Verifica si ya existe una transacción activa
            if (dbContext.HasActiveTransaction) 
                return await next();

            // Crea una estrategia de ejecución para la base de datos.
            var strategy = dbContext.Database.CreateExecutionStrategy();

            // Ejecuta la estrategia con una nueva transacción
            await strategy.ExecuteAsync(async () =>
            {
                // Inicia una nueva transacción 
                await using var transaction = await dbContext.BeginTransactionAsync();
                
                using (logger.BeginScope(new List<KeyValuePair<string, object>>
                           { new("TransactionContext", transaction.TransactionId) }))
                {
                    logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command})",
                        transaction.TransactionId, typeName, request);

                    // Procesa la solicitud
                    response = await next();

                    logger.LogInformation("Commit transaction {TransactionId} for {CommandName}",
                        transaction.TransactionId, typeName);

                    // Confirma la transacción
                    await dbContext.CommitTransactionAsync();
                }
            });

            return response;
        }
        catch (Exception ex)
        {
            //  Registro del error en el logger
            logger.LogError(ex, "Error Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}