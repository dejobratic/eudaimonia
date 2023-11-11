using Eudaimonia.Domain;

namespace Eudaimonia.Application.Utils.Repositories;

public interface IAuthorRepository : ICommandRepository<Author, AuthorId>
{
}