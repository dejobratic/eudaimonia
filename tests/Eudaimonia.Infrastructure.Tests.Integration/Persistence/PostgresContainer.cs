using Testcontainers.PostgreSql;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence;

public class PostgresContainer : IDbContainer
{
    private readonly PostgreSqlContainer _container;

    public PostgresContainer()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:14.1-alpine")
            .WithCleanUp(true)
            .Build();
    }

    public string GetConnectionString()
        => _container.GetConnectionString();

    public async Task InitializeAsync()
        => await _container.StartAsync();

    public async Task DisposeAsync()
        => await _container.DisposeAsync();
}