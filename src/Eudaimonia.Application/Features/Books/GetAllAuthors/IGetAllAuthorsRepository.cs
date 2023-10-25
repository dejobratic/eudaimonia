using Eudaimonia.Application.Utils.Dtos;

namespace Eudaimonia.Application.Features.Books.GetAllAuthors;

public interface IGetAllAuthorsRepository
{
    Task<IEnumerable<AuthorDto>> GetAllAsync(CancellationToken cancellationToken = default);
}