using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Eudaimonia.Application.Features.Books.AddAuthor;
using Eudaimonia.Application.Features.Books.AddBook;
using Eudaimonia.Application.Features.Books.AddPublisher;
using Eudaimonia.Application.Features.Books.GetAllBooks;
using Eudaimonia.Infrastructure.Persistence.Repositories;
using Eudaimonia.Infrastructure.Persistence.Postgres;

namespace Eudaimonia.Infrastructure;

public static class DependencyInjection
{
    static class DatabaseConstants
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
        services.AddScoped<IAddPublisherRepository, PublisherCommandRepository>();
        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseProvider = configuration[DatabaseConstants.DatabaseProvider]?.ToLowerInvariant();

        switch (databaseProvider)
        {
            default:
                services.AddDbContext<DbContext, PostgresDbContext>(
                    o => o.UseNpgsql(configuration[DatabaseConstants.PostgresConnectionString],
                    b => b.MigrationsAssembly(typeof(PostgresDbContext).Assembly.FullName)));
                break;
        }

        return services;
    }
}
