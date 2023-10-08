using Eudaimonia.Domain;

namespace Eudaimonia.Application.Dtos;

public interface IBookFactory<in T>
    where T : ICommand
{
    Book CreateFrom(T command);
}
