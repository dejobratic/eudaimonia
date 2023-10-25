using Eudaimonia.Infrastructure.Persistence.Queries;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries;

[Collection("QueryDatabase")]
public class QueryDbTestsBase : DbTestsBase<QueryDbContext>, IAsyncLifetime
{
    public QueryDbTestsBase(QueryDbFixture fixture)
        : base(fixture)
    {
    }

    public async Task InitializeAsync()
        => await DbContext.Database.BeginTransactionAsync();

    public async Task DisposeAsync()
        => await DbContext.Database.RollbackTransactionAsync();
}