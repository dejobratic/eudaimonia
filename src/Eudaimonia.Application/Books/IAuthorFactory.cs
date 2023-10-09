using Eudaimonia.Domain;

namespace Eudaimonia.Application.Books;

public interface IAuthorFactory<in T>
    where T : ICommand
{
    Author CreateFrom(T command);
}
