﻿namespace Eudaimonia.Application.Utils.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<CommandResult> DispatchAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        var (handler, handlerType) = ResolveCommandHandler(command);

        var handleMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand>.HandleAsync));
        return await (Task<CommandResult>)handleMethod!.Invoke(handler, new object[] { command, cancellationToken })!;
    }

    private (object, Type) ResolveCommandHandler(ICommand command)
    {
        var commandType = command.GetType();
        var commandHandlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);

        var commandHandler = _serviceProvider.GetService(commandHandlerType)
            ?? throw new InvalidOperationException($"No command handler found for command type {commandType.Name}.");

        return (commandHandler, commandHandlerType);
    }
}