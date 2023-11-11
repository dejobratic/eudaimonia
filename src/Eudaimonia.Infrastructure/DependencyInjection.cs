using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Infrastructure.Persistence;
using Eudaimonia.Infrastructure.Persistence.Commands;
using Eudaimonia.Infrastructure.Persistence.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eudaimonia.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddAll(typeof(ICommandRepository<,>), ServiceLifetime.Transient);
        services.AddAll(typeof(IQueryRepository<,>), ServiceLifetime.Transient);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseProvider = configuration[DbOptions.DatabaseProvider]?.ToLowerInvariant();

        switch (databaseProvider)
        {
            default:
                services.AddDbContext<CommandDbContext>();
                services.AddDbContext<QueryDbContext>();
                break;
        }

        return services;
    }

    private static IServiceCollection AddAll(
        this IServiceCollection services,
        Type type,
        ServiceLifetime lifetime)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(type)
            .AddClasses(classes => classes.AssignableTo(type))
            .AsImplementedInterfaces()
            .WithLifetime(lifetime));
    }
}