using Eudaimonia.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eudaimonia.Infrastructure.Tests.Integration.Postgres.Repositories;

public abstract class RepositoryTestBase
{
    private readonly PostgresFixture _dbFixture;

    protected RepositoryTestBase(PostgresFixture fixture)
    {
        _dbFixture = fixture;
    }

    protected PostgresDbContext DbContext => _dbFixture.DbContext;

    protected async Task<T?> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        => await DbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);

    protected async Task AddAsync<T>(T entity) where T : class
        => await DbContext.AddAsync(entity);

    protected async Task SaveChangesAsync()
        => await DbContext.SaveChangesAsync();
}