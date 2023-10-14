using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence;

public abstract class DbFixture<T> : IAsyncLifetime
    where T : DbContext
{
    private readonly PostgresContainer _container;

    private T? _dbContext;
    public T DbContext => _dbContext ??= CreateDbContext();

    protected DbFixture()
    {
        _container = new PostgresContainer();
    }

    public async Task InitializeAsync()
    {
        await _container.InitializeAsync();
        _dbContext ??= CreateDbContext();
    }

    public async Task DisposeAsync()
        => await _container.DisposeAsync();

    protected string GetConnectionString()
        => _container.GetConnectionString();

    protected abstract T CreateDbContext();
}