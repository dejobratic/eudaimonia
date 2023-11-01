using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence;

public abstract class DbTestsBase<T>
    where T : DbContext
{
    private readonly DbFixture<T> _dbFixture;

    protected DbTestsBase(DbFixture<T> fixture)
    {
        _dbFixture = fixture;
    }

    protected T DbContext => _dbFixture.DbContext;

    protected async Task<TEntity?> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        => await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

    protected async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        => await DbContext.AddAsync(entity);

    protected async Task DeleteAllAsync<TEntity>() where TEntity : class
        => await DbContext.Set<TEntity>().ExecuteDeleteAsync();

    protected async Task SaveChangesAsync()
        => await DbContext.SaveChangesAsync();
}