using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books.AddAuthor;

public interface IAddAuthorRepository
{
    Task AddAsync(Author author, CancellationToken cancellationToken = default);
}