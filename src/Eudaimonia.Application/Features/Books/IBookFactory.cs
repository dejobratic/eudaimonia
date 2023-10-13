using Eudaimonia.Application.Utils;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books;

public interface IBookFactory<in T>
    where T : ICommand
{
    Book CreateFrom(T command);
}