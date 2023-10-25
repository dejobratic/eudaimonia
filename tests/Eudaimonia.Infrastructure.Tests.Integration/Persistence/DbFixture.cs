using Eudaimonia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence;

public abstract class DbFixture<T> : IAsyncLifetime
    where T : DbContext
{
    private readonly PostgresContainer _container;

    private T? _dbContext;
    public T DbContext => _dbContext ??= CreateDbContext();

    protected IConfigurationRoot _configuration = null!;

    protected DbFixture()
    {
        _container = new PostgresContainer();
    }

    public async Task InitializeAsync()
    {
        await _container.InitializeAsync();
        _configuration ??= CreateConfiguration();
        _dbContext ??= CreateDbContext();
    }

    public async Task DisposeAsync()
        => await _container.DisposeAsync();

    private IConfigurationRoot CreateConfiguration()
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [DbOptions.PostgresConnectionString] = _container.GetConnectionString()
            })
            .Build();
    }

    protected abstract T CreateDbContext();
}