using Eudaimonia.Domain;

namespace Eudaimonia.Application.Books.AddAuthor;

public interface IAddAuthorRepository
{
    Task AddAsync(Author author, CancellationToken cancellationToken = default);
}
