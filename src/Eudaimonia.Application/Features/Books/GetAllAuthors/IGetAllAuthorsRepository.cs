using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books.GetAllAuthors;

public interface IGetAllAuthorsRepository
{
    Task<IEnumerable<AuthorDto>> GetAllAsync(CancellationToken cancellationToken = default);
}