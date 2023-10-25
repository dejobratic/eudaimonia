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

        modelBuilder.EntitySet<BookDto>("Books");
        modelBuilder.EntityType<BookDto>().HasKey(x => x.Id);
        modelBuilder.EntityType<BookDto>().ContainsRequired(a => a.Author).AutoExpand = true;

        modelBuilder.EntitySet<PublisherDto>("Publishers");
        modelBuilder.EntityType<PublisherDto>().HasKey(x => x.Id);
        modelBuilder.EntityType<PublisherDto>().ContainsMany(a => a.PublishedBooks).AutoExpand = true;

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
        services.AddAllAsTransient(typeof(ICommandHandler<>));
        services.AddAllAsTransient(typeof(IQueryHandler<,>));
        services.AddAllAsTransient(typeof(IFactory<,>));

        services.AddTransient<ICommandDispatcher, CommandDispatcher>();
        services.AddTransient<IQueryDispatcher, QueryDispatcher>();

        return services;
    }

    private static IServiceCollection AddAllAsTransient(
        this IServiceCollection services,
        Type type)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(type)
            .AddClasses(classes => classes.AssignableTo(type))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }
}
