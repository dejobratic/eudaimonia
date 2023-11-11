using Eudaimonia.Application.Dtos;
using Eudaimonia.Presentation.Exceptions;
using Eudaimonia.Presentation.GraphQL.Schemas;
using GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.ModelBuilder;

namespace Eudaimonia.Presentation;

public static class DependencyInjection
{
    private const string ODataControllerNamespace = "Eudaimonia.Presentation.OData.Controllers";
    private const string GraphQLControllerNamespace = "Eudaimonia.Presentation.GraphQL.Controllers";

    public static WebApplication ConfigurePresentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            var provider = app.Configuration[PresentationOptions.Provider];
            switch (provider?.ToLowerInvariant())
            {
                case PresentationOptions.GraphQL:
                    app.UseGraphQLGraphiQL();
                    app.UseGraphQLAltair();
                    break;

                case PresentationOptions.OData:
                    app.UseSwagger();
                    app.UseSwaggerUI();
                    break;
            }
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    public static IServiceCollection AddPresentationDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var provider = configuration[PresentationOptions.Provider];

        switch (provider?.ToLowerInvariant())
        {
            case PresentationOptions.GraphQL:
                services.AddGraphQLDependencies();
                services.AddControllersFromNamespace(GraphQLControllerNamespace);
                break;

            case PresentationOptions.OData:
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
                services.AddControllersFromNamespace(ODataControllerNamespace).AddODataDependencies();
                break;

            default:
                throw new UnsupportedConfigurationException(PresentationOptions.Provider, provider);
        }

        return services;
    }

    private static IServiceCollection AddGraphQLDependencies(
        this IServiceCollection services)
    {
        services.AddGraphQL(b => b
            .AddSystemTextJson()
            .AddSchema<RootSchema>()
            .AddGraphTypes(typeof(RootSchema).Assembly));

        return services;
    }

    private static IMvcBuilder AddODataDependencies(this IMvcBuilder builder)
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
            opt.Select().Filter().Expand().OrderBy().Count().SetMaxTop(50).AddRouteComponents("api", modelBuilder.GetEdmModel());
        });

        return builder;
    }

    public static IMvcBuilder AddControllersFromNamespace(this IServiceCollection services, string namespaceToInclude)
        => services.AddControllers(opt => opt.Conventions.Add(new NamespaceBasedControllerModelConvention(namespaceToInclude)));
}
