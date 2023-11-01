using Eudaimonia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence;

public abstract class DbFixture<T> : IAsyncLifetime
    where T : DbContext
{
    private readonly IDbContainer _dbContainer;
    protected IConfigurationRoot _configuration = null!;

    private T _dbContext = null!;
    public T DbContext => _dbContext ??= CreateDbContext();

    protected DbFixture()
    {
        _dbContainer = new PostgresContainer();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.InitializeAsync();

        _configuration ??= CreateConfiguration();
        _dbContext ??= CreateDbContext();
    }

    public async Task DisposeAsync()
    {
        await _dbContext!.DisposeAsync();
        await _dbContainer.DisposeAsync();
    }

    private IConfigurationRoot CreateConfiguration()
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [DbOptions.PostgresConnectionString] = _dbContainer.GetConnectionString()
            })
            .Build();
    }

    protected abstract T CreateDbContext();
}