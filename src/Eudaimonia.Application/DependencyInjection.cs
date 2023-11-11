using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Queries;
using Microsoft.Extensions.DependencyInjection;
using Eudaimonia.Domain.Factories;
using Eudaimonia.Domain.Kernel;
using Eudaimonia.Application.Utils;

namespace Eudaimonia.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(
        this IServiceCollection services)
    {
        services.AddAll(typeof(ICommandHandler<>), ServiceLifetime.Transient);
        services.AddAll(typeof(IQueryHandler<,>), ServiceLifetime.Transient);
        services.AddAll(typeof(IEntityFactory), ServiceLifetime.Transient);

        services.AddTransient<IIdGenerator<Guid>, GuidGenerator>();

        services.AddTransient<ICommandDispatcher, CommandDispatcher>();
        services.AddTransient<IQueryDispatcher, QueryDispatcher>();

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
