using Eudaimonia.Infrastructure.Persistence.Commands;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands;

public class CommandDbFixture : DbFixture<CommandDbContext>
{
    protected override CommandDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<CommandDbContext>()
            .UseNpgsql(GetConnectionString(), b => b.MigrationsAssembly(typeof(CommandDbContext).Assembly.FullName))
            .Options;

        var dbContext = new CommandDbContext(options);
        dbContext.Database.Migrate();

        return dbContext;
    }
}