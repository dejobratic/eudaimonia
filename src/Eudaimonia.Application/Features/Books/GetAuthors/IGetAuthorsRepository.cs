using Eudaimonia.Application.Utils.Dtos;

namespace Eudaimonia.Application.Features.Books.GetAuthors;

public interface IGetAuthorsRepository
{
    Task<IEnumerable<AuthorDto>> GetAsync(CancellationToken cancellationToken = default);
}