using Eudaimonia.Application.Utils.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Eudaimonia.Application.Tests.Unit.Utils;

public class CommandDispatcherTests
{
    private class TestCommand1 : ICommand { }
    private class TestCommand2 : ICommand { }
    private class TestCommand3 : ICommand { }

    private class TestCommand1Handler : ICommandHandler<TestCommand1>
    {
        public Task<CommandResult> HandleAsync(TestCommand1 command, CancellationToken cancellationToken = default)
            => Task.FromResult(new CommandResult(nameof(TestCommand1)));
    }

    private class TestCommand2Handler : ICommandHandler<TestCommand2>
    {
        public Task<CommandResult> HandleAsync(TestCommand2 command, CancellationToken cancellationToken = default)
            => Task.FromResult(new CommandResult(nameof(TestCommand2)));
    }

    private class TestCommand2Handler2 : ICommandHandler<TestCommand2>
    {
        public Task<CommandResult> HandleAsync(TestCommand2 command, CancellationToken cancellationToken = default)
            => Task.FromResult(new CommandResult(nameof(TestCommand2)));
    }

    private readonly CommandDispatcher _sut;

    public CommandDispatcherTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<ICommandHandler<TestCommand1>, TestCommand1Handler>()
            .AddScoped<ICommandHandler<TestCommand2>, TestCommand2Handler>()
            .AddScoped<ICommandHandler<TestCommand2>, TestCommand2Handler2>()
            .BuildServiceProvider();

        _sut = new CommandDispatcher(serviceProvider);
    }

    [Fact]
    public async Task DispatchAsync_WithCommand1_ReturnsCommand1Result()
    {
        // Arrange
        var command = new TestCommand1();

        // Act
        var result = await _sut.DispatchAsync(command);

        // Assert
        Assert.Equal(nameof(TestCommand1), result.Data);
    }

    [Fact]
    public async Task DispatchAsync_WithCommand2_ReturnsCommand2Result()
    {
        // Arrange
        var command = new TestCommand2();

        // Act
        var result = await _sut.DispatchAsync(command);

        // Assert
        Assert.Equal(nameof(TestCommand2), result.Data);
    }

    [Fact]
    public async Task DispatchAsync_WithCommand3_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = new TestCommand3();

        // Act
        Task<CommandResult> action() => _sut.DispatchAsync(command);

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>((Func<Task<CommandResult>>)action);
        Assert.Equal($"No command handler found for command type TestCommand3.", exception.Message);
    }
}
