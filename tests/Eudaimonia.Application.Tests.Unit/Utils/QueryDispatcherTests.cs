using Eudaimonia.Application.Utils.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Eudaimonia.Application.Tests.Unit.Utils;

public class QueryDispatcherTests
{
    private class TestQuery1 : IQuery { }
    private class TestQuery2 : IQuery { }
    private class TestQuery3 : IQuery { }
    private class TestQuery1Handler : IQueryHandler<TestQuery1, string>
    {
        public Task<string> HandleAsync(TestQuery1 query)
            => Task.FromResult(nameof(TestQuery1));
    }

    private class TestQuery2Handler : IQueryHandler<TestQuery2, string>
    {
        public Task<string> HandleAsync(TestQuery2 query)
            => Task.FromResult(nameof(TestQuery2));
    }

    private class TestQuery2Handler2 : IQueryHandler<TestQuery2, string>
    {
        public Task<string> HandleAsync(TestQuery2 query)
            => Task.FromResult(nameof(TestQuery2));
    }

    private readonly QueryDispatcher _sut;

    public QueryDispatcherTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<IQueryHandler<TestQuery1, string>, TestQuery1Handler>()
            .AddScoped<IQueryHandler<TestQuery2, string>, TestQuery2Handler>()
            .AddScoped<IQueryHandler<TestQuery2, string>, TestQuery2Handler2>()
            .BuildServiceProvider();

        _sut = new QueryDispatcher(serviceProvider);
    }

    [Fact]
    public async Task DispatchAsync_WithQuery1_ReturnsQuery1Result()
    {
        // Arrange
        var query = new TestQuery1();

        // Act
        var result = await _sut.DispatchAsync<string>(query);

        // Assert
        Assert.Equal(nameof(TestQuery1), result);
    }

    [Fact]
    public async Task DispatchAsync_WithQuery2_ReturnsQuery2Result()
    {
        // Arrange
        var query = new TestQuery2();

        // Act
        var result = await _sut.DispatchAsync<string>(query);

        // Assert
        Assert.Equal(nameof(TestQuery2), result);
    }

    [Fact]
    public async Task DispatchAsync_WithQuery3_ThrowsInvalidOperationException()
    {
        // Arrange
        var query = new TestQuery3();

        // Act
        Task<string> action() => _sut.DispatchAsync<string>(query);

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(action);
        Assert.Equal($"No query handler found for query type TestQuery3.", exception.Message);
    }
}
