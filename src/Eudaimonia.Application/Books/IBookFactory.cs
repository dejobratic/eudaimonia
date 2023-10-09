using Eudaimonia.Domain;

namespace Eudaimonia.Application.Books;

public interface IBookFactory<in T>
    where T : ICommand
{
    Book CreateFrom(T command);
}