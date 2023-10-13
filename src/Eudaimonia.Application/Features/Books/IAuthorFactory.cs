using Eudaimonia.Application.Utils;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books;

public interface IAuthorFactory<in T>
    where T : ICommand
{
    Author CreateFrom(T command);
}