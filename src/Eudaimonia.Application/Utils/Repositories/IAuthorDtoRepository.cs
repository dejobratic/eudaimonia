using Eudaimonia.Application.Dtos;

namespace Eudaimonia.Application.Utils.Repositories;

public interface IAuthorDtoRepository : IQueryRepository<AuthorDto, Guid>
{
}