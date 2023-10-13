using Eudaimonia.Application.Utils;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books;

public interface IPublisherFactory<in T>
    where T : ICommand
{
    Publisher CreateFrom(T command);
}