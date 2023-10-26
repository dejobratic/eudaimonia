namespace Eudaimonia.Application.Utils.Commands;

public interface ICommandDispatcher
{
    Task<CommandResult> DispatchAsync(ICommand command);
}