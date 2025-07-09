using Eudaimonia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence;

public abstract class DbFixture<T> : IAsyncLifetime
    where T : DbContext
{
    private readonly PostgresContainer _dbContainer = new();
    
    protected IConfigurationRoot Configuration = null!;

    private T _dbContext = null!;
    public T DbContext => _dbContext ??= CreateDbContext();

    public async Task InitializeAsync()
    {
        await _dbContainer.InitializeAsync();

        Configuration ??= CreateConfiguration();
        _dbContext ??= CreateDbContext();
    }

    public virtual async Task DisposeAsync()
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