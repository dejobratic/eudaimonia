using Eudaimonia.Application;
using Eudaimonia.Application.Books;
using Eudaimonia.Application.Books.AddAuthor;
using Eudaimonia.Application.Books.AddBook;
using Eudaimonia.Application.Books.AddPublisher;
using Eudaimonia.Application.Books.GetAllBooks;
using Eudaimonia.Application.Dtos;
using Eudaimonia.Infrastructure;

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
        services.AddScoped<IQueryHandler<GetAllBooksQuery, IEnumerable<BookDto>>, GetAllBooksQueryHandler>();

        services.AddScoped<ICommandHandler<AddAuthorCommand>, AddAuthorCommandHandler>();
        services.AddScoped<IAuthorFactory<AddAuthorCommand>, AddAuthorCommandAuthorFactory>();
        services.AddScoped<ICommandHandler<AddPublisherCommand>, AddPublisherCommandHandler>();
        services.AddScoped<IPublisherFactory<AddPublisherCommand>, AddPublisherCommandPublisherFactory>();

        return services;
    }
}
