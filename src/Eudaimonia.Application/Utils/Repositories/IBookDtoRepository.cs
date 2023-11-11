using Eudaimonia.Application.Dtos;

namespace Eudaimonia.Application.Utils.Repositories;

public interface IBookDtoRepository : IQueryRepository<BookDto, Guid>
{
}