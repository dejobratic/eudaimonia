using Eudaimonia.Infrastructure.Persistence.Commands;
using Eudaimonia.Infrastructure.Persistence.Queries;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries;

[Collection("QueryDatabase")]
public class QueryDbTestsBase(QueryDbFixture fixture) : DbTestsBase<QueryDbContext>(fixture)
{
    private CommandDbContext CommandDbContext => fixture.CommandDbContext;

    protected override async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        => await CommandDbContext.AddAsync(entity);

    protected override async Task SaveChangesAsync()
        => await CommandDbContext.SaveChangesAsync();
}