namespace Eudaimonia.Application.Utils.Commands;

public record CommandResult(object? Data = null);

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<CommandResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}