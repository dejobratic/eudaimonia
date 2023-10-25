using Eudaimonia.Application.Features.Books;
using Eudaimonia.Application.Features.Books.AddAuthor;
using Eudaimonia.Application.Features.Books.AddBook;
using Eudaimonia.Application.Features.Books.AddPublisher;
using Eudaimonia.Application.Features.Books.GetAllAuthors;
using Eudaimonia.Application.Features.Books.GetAllBooks;
using Eudaimonia.Application.Features.Books.GetAllPublishers;
using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Dtos;
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
        services.AddScoped<ICommandHandler<AddBookCommand>, AddBookCommandHandler>();
        services.AddScoped<IBookFactory<AddBookCommand>, AddBookCommandBookFactory>();
        services.AddScoped<IQueryHandler<GetAllBooksQuery, IEnumerable<BookDto>>, GetAllBooksQueryHandler>();

        services.AddScoped<ICommandHandler<AddAuthorCommand>, AddAuthorCommandHandler>();
        services.AddScoped<IAuthorFactory<AddAuthorCommand>, AddAuthorCommandAuthorFactory>();
        services.AddScoped<IQueryHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>, GetAllAuthorsQueryHandler>();

        services.AddScoped<ICommandHandler<AddPublisherCommand>, AddPublisherCommandHandler>();
        services.AddScoped<IPublisherFactory<AddPublisherCommand>, AddPublisherCommandPublisherFactory>();
        services.AddScoped<IQueryHandler<GetAllPublishersQuery, IEnumerable<PublisherDto>>, GetAllPublishersQueryHandler>();

        return services;
    }
}
