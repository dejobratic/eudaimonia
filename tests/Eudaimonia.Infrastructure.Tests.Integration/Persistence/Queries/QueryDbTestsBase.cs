using Eudaimonia.Infrastructure.Persistence.Queries;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries;

[Collection("QueryDatabase")]
public class QueryDbTestsBase : DbTestsBase<QueryDbContext>
{
    public QueryDbTestsBase(QueryDbFixture fixture)
        : base(fixture)
    {
    }
}