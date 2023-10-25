using Eudaimonia.Infrastructure.Persistence.Commands;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands;

public class CommandDbFixture : DbFixture<CommandDbContext>
{
    protected override CommandDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<CommandDbContext>()
            .Options;

        var dbContext = new CommandDbContext(options, _configuration);
        dbContext.Database.Migrate();

        return dbContext;
    }
}