using Eudaimonia.Application.Features.Books;
using Eudaimonia.Application.Features.Books.AddAuthor;
using Eudaimonia.Application.Features.Books.AddBook;
using Eudaimonia.Application.Features.Books.AddPublisher;
using Eudaimonia.Application.Features.Books.GetAllBooks;
using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Dtos;
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
