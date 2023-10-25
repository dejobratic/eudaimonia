using Eudaimonia.Application.Features.Books.AddAuthor;
using Eudaimonia.Application.Features.Books.AddBook;
using Eudaimonia.Application.Features.Books.AddPublisher;
using Eudaimonia.Application.Features.Books.GetAllAuthors;
using Eudaimonia.Application.Features.Books.GetAllBooks;
using Eudaimonia.Application.Features.Books.GetAllPublishers;
using Eudaimonia.Application.Utils;
using Eudaimonia.Infrastructure.Persistence;
using Eudaimonia.Infrastructure.Persistence.Commands;
using Eudaimonia.Infrastructure.Persistence.Commands.Repositories;
using Eudaimonia.Infrastructure.Persistence.Queries;
using Eudaimonia.Infrastructure.Persistence.Queries.Repositories;
using Microsoft.EntityFrameworkCore;
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
        services.AddScoped<IAddBookRepository, BookCommandRepository>();
        services.AddScoped<IGetAllBooksRepository, BookQueryRepository>();
        services.AddScoped<IAddAuthorRepository, AuthorCommandRepository>();
        services.AddScoped<IGetAllAuthorsRepository, AuthorQueryRepository>();
        services.AddScoped<IAddPublisherRepository, PublisherCommandRepository>();
        services.AddScoped<IGetAllPublishersRepository, PublisherQueryRepository>();
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
}
