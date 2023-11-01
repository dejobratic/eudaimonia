using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Queries;
using Eudaimonia.Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

namespace Eudaimonia.Api;

public static class DependencyInjection
{
    public static IMvcBuilder AddODataDependencies(this IMvcBuilder builder)
    {
        var modelBuilder = new ODataConventionModelBuilder();

        modelBuilder.EntitySet<AuthorDto>("Authors");
        modelBuilder.EntityType<AuthorDto>().HasKey(x => x.Id);
        modelBuilder.EntityType<AuthorDto>().ContainsMany(a => a.AuthoredBooks).AutoExpand = true;

        modelBuilder.EntitySet<PublisherDto>("Publishers");
        modelBuilder.EntityType<PublisherDto>().HasKey(x => x.Id);
        modelBuilder.EntityType<PublisherDto>().ContainsMany(a => a.PublishedEditions).AutoExpand = true;

        modelBuilder.EntitySet<BookDto>("Books");
        modelBuilder.EntityType<BookDto>().HasKey(x => x.Id);
        modelBuilder.EntityType<BookDto>().ContainsRequired(a => a.Author).AutoExpand = true;
        modelBuilder.EntityType<BookDto>().ContainsRequired(a => a.DefaultEdition).AutoExpand = true;

        modelBuilder.EntitySet<EditionDto>("Editions");
        modelBuilder.EntityType<EditionDto>().HasKey(x => x.Id);
        modelBuilder.EntityType<EditionDto>().ContainsRequired(a => a.Book).AutoExpand = true;
        modelBuilder.EntityType<EditionDto>().ContainsRequired(a => a.Publisher).AutoExpand = true;

        builder.AddOData(opt =>
        {
            opt.Select().Filter().Expand().OrderBy().Count().SetMaxTop(null).AddRouteComponents("api", modelBuilder.GetEdmModel());
        });

        return builder;
    }

    public static IServiceCollection AddProjectDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplicationDependencies();
        services.AddInfrastructureDependencies(configuration);
        return services;
    }

    public static IServiceCollection AddApplicationDependencies(
        this IServiceCollection services)
    {
        services.AddAll(typeof(ICommandHandler<>), ServiceLifetime.Transient);
        services.AddAll(typeof(IQueryHandler<,>), ServiceLifetime.Transient);
        services.AddAll(typeof(IFactory<,>), ServiceLifetime.Transient);

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
