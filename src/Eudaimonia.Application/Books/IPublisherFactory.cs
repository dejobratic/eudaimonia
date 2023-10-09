using Eudaimonia.Domain;

namespace Eudaimonia.Application.Books;

public interface IPublisherFactory<in T>
    where T : ICommand
{
    Publisher CreateFrom(T command);
}