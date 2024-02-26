using Cf.Dotnet.Architecture.Domain.SeedWork;
using Cf.Dotnet.Architecture.Infrastructure.Abstractions;
using Cf.DotnetArchitecture.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cf.Dotnet.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration,
        string sectionName = "SqlServer")
    {
        services
            .AddScoped<IDatabaseContext, DatabaseContext>()
            .AddDbContext<DatabaseContext>(config =>
            {
                config.UseSqlServer(configuration.GetConnectionString(sectionName));
            });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, DatabaseContext>();
        return services;
    }
}