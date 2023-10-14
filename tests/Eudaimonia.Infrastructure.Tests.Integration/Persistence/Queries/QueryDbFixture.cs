using Eudaimonia.Infrastructure.Persistence.Commands;
using Eudaimonia.Infrastructure.Persistence.Queries;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries;

public class QueryDbFixture : DbFixture<QueryDbContext>
{
    protected override QueryDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<QueryDbContext>()
            .UseNpgsql(GetConnectionString())
            .Options;

        MigrateDatabase();
        return new QueryDbContext(options);
    }

    private void MigrateDatabase()
    {
        var options = new DbContextOptionsBuilder<CommandDbContext>()
            .UseNpgsql(GetConnectionString(), b => b.MigrationsAssembly(typeof(CommandDbContext).Assembly.FullName))
            .Options;

        using var dbContext = new CommandDbContext(options);
        dbContext.Database.Migrate();
    }
}