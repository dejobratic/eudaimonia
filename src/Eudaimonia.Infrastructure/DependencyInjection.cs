using Eudaimonia.Application.Books.AddBook;
using Eudaimonia.Application.Books.GetAllBooks;
using Eudaimonia.Infrastructure.Postgres.Repositories;
using Eudaimonia.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Eudaimonia.Application.Books.AddAuthor;
using Eudaimonia.Application.Books.AddPublisher;

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
        services.AddScoped<IAddPublisherRepository, PublisherCommandRepository>();
        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["DatabaseOptions:PostgresOptions:ConnectionString"];
        var migrationsAssembly = typeof(PostgresDbContext).Assembly.FullName;

        services.AddDbContext<DbContext, PostgresDbContext>(
            o => o.UseNpgsql(connectionString, b => b.MigrationsAssembly(migrationsAssembly)));

        return services;
    }
}
