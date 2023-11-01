using Eudaimonia.Infrastructure.Persistence.Commands;
using Eudaimonia.Infrastructure.Persistence.Queries;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries;

public class QueryDbFixture : DbFixture<QueryDbContext>
{
    protected override QueryDbContext CreateDbContext()
    {
        MigrateDatabase();
        return CreateQueryDbContext();
    }

    private QueryDbContext CreateQueryDbContext()
    {
        var options = new DbContextOptionsBuilder<QueryDbContext>()
            .Options;

        return new QueryDbContext(options, _configuration);
    }

    private void MigrateDatabase()
    {
        var options = new DbContextOptionsBuilder<CommandDbContext>()
            .Options;

        using var dbContext =  new CommandDbContext(options, _configuration);
        dbContext.Database.Migrate();
    }
}