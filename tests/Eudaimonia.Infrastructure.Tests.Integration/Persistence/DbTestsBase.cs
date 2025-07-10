using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence;

public abstract class DbTestsBase<T>(DbFixture<T> fixture) : IAsyncLifetime
    where T : DbContext
{
    protected T DbContext => fixture.DbContext;

    public async Task InitializeAsync()
        => await CleanupDatabaseAsync();

    public async Task DisposeAsync()
        => await CleanupDatabaseAsync();

    private async Task CleanupDatabaseAsync()
        => await DbContext.Database
            .ExecuteSqlRawAsync("TRUNCATE TABLE \"Reviews\", \"Editions\", \"Books\", \"Publishers\", \"Authors\" RESTART IDENTITY CASCADE");

    protected async Task<TEntity?> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        => await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

    protected virtual async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        => await DbContext.AddAsync(entity);

    protected virtual async Task DeleteAllAsync<TEntity>() where TEntity : class
        => await DbContext.Set<TEntity>().ExecuteDeleteAsync();

    protected virtual async Task SaveChangesAsync()
        => await DbContext.SaveChangesAsync();
}