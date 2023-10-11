using Eudaimonia.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Tests.Integration.Postgres;

public class PostgresFixture : IAsyncLifetime
{
    private readonly PostgresContainer _container;

    private PostgresDbContext? _dbContext;
    public PostgresDbContext DbContext => _dbContext ??= CreateDbContext();

    public PostgresFixture()
    {
        _container = new PostgresContainer();
    }

    public async Task InitializeAsync()
        => await _container.InitializeAsync();

    public async Task DisposeAsync()
        => await _container.DisposeAsync();

    private PostgresDbContext CreateDbContext()
    {
        var options = CreateDbContextOptions();
        var dbContext = new PostgresDbContext(options);
        dbContext.Database.Migrate();

        return dbContext;
    }

    private DbContextOptions<PostgresDbContext> CreateDbContextOptions()
    {
        var connectionString = _container.GetConnectionString();
        var migrationsAssembly = typeof(PostgresDbContext).Assembly.FullName;

        return new DbContextOptionsBuilder<PostgresDbContext>()
            .UseNpgsql(connectionString, b => b.MigrationsAssembly(migrationsAssembly))
            .Options;
    }
}