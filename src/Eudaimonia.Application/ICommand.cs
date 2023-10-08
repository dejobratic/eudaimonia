namespace Eudaimonia.Application;

public interface ICommand
{
}

public record CommandResult(object? Data = null);

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<CommandResult> HandleAsync(TCommand command);
}

public interface ICommandValidator<in TCommand>
    where TCommand : ICommand
{
    Task ValidateAsync(TCommand command);
}