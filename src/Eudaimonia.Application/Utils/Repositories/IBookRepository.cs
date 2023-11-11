using Eudaimonia.Domain;

namespace Eudaimonia.Application.Utils.Repositories;

public interface IBookRepository : ICommandRepository<Book, BookId>
{
}