using Eudaimonia.Application.Features.Books.AddAuthor;
using Eudaimonia.Application.Features.Books.AddBook;
using Eudaimonia.Application.Features.Books.AddPublisher;
using Eudaimonia.Application.Features.Books.GetAllAuthors;
using Eudaimonia.Application.Features.Books.GetAllBooks;
using Eudaimonia.Application.Utils;
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
    private static class DatabaseConstants
    {
        public const string DatabaseProvider = "DatabaseOptions:Provider";
        public const string PostgresConnectionString = "DatabaseOptions:PostgresOptions:ConnectionString";
    }

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
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseProvider = configuration[DatabaseConstants.DatabaseProvider]?.ToLowerInvariant();

        switch (databaseProvider)
        {
            default:
                services.AddDbContext<CommandDbContext>(
                    o => o.UseNpgsql(configuration[DatabaseConstants.PostgresConnectionString],
                    b => b.MigrationsAssembly(typeof(CommandDbContext).Assembly.FullName)));

                services.AddDbContext<QueryDbContext>(
                    o =>
                    {
                        o.UseNpgsql(configuration[DatabaseConstants.PostgresConnectionString]);
                        o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    });
                break;
        }

        return services;
    }
}