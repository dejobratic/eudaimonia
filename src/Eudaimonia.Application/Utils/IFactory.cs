using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Application.Utils;

public interface IFactory<in TCommand, out TEntity>
    where TCommand : ICommand
    //where TEntity : Entity<> TODO: figure this out
{
    TEntity CreateFrom(TCommand command);
}