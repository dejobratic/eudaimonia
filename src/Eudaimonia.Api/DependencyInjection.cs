using Eudaimonia.Application;
using Eudaimonia.Application.Books.AddBook;
using Eudaimonia.Application.Books.GetAllBooks;
using Eudaimonia.Application.Dtos;
using Eudaimonia.Infrastructure.Postgres;
using Eudaimonia.Infrastructure.Postgres.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Api;

public static class DependencyInjection
{
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

        services.AddTransient<IQueryHandler<GetAllBooksQuery, IEnumerable<BookDto>>, GetAllBooksQueryHandler>();

        return services;
    }

    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddScoped<IAddBookRepository, BookCommandRepository>();
        services.AddScoped<IGetAllBooksRepository, BookQueryRepository>();
        return services;
    }

    public static IServiceCollection AddDatabase(
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
