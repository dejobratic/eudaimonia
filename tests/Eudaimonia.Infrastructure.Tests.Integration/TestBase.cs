using Eudaimonia.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Testcontainers.PostgreSql;

namespace Eudaimonia.Infrastructure.Tests.Integration;

public abstract class TestBase : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;

    private DbContext? _dbContext;
    public DbContext DbContext => _dbContext ??= CreateDbContext();

    protected TestBase()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:14.1-alpine")
            .WithCleanUp(true)
            .Build();
    }

    public async Task AddAsync<T>(T entity)
        where T : class
    {
        await DbContext.AddAsync(entity);
    }

    public async Task SaveChangesAsync()
        => await DbContext.SaveChangesAsync();

    public async Task<T?> FindAsync<T>(Expression<Func<T, bool>> predicate)
        where T : class
    {
        return await DbContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate);
    }

    public string GetConnectionString()
        => _container.GetConnectionString();

    public async Task InitializeAsync()
        => await _container.StartAsync();

    public async Task DisposeAsync()
        => await _container.DisposeAsync();

    private DbContext CreateDbContext()
    {
        var options = CreateDbContextOptions();
        var dbContext = new PostgresDbContext(options);
        dbContext.Database.Migrate();

        return dbContext;
    }

    private DbContextOptions<PostgresDbContext> CreateDbContextOptions()
    {
        var connectionString = GetConnectionString();
        var migrationsAssembly = typeof(PostgresDbContext).Assembly.FullName;

        return new DbContextOptionsBuilder<PostgresDbContext>()
            .UseNpgsql(connectionString, b => b.MigrationsAssembly(migrationsAssembly))
            .Options;
    }
}