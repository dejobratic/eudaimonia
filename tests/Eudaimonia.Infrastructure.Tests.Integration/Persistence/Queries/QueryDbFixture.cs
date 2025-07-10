using Eudaimonia.Infrastructure.Persistence.Commands;
using Eudaimonia.Infrastructure.Persistence.Queries;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries;

public class QueryDbFixture : DbFixture<QueryDbContext>
{
    private CommandDbContext? _commandDbContext;
    public CommandDbContext CommandDbContext => _commandDbContext ??= CreateCommandDbContext();

    protected override QueryDbContext CreateDbContext()
    {
        MigrateDatabase();
        return CreateQueryDbContext();
    }

    private QueryDbContext CreateQueryDbContext()
    {
        var options = new DbContextOptionsBuilder<QueryDbContext>()
            .Options;

        return new QueryDbContext(options, Configuration);
    }

    private CommandDbContext CreateCommandDbContext()
    {
        var options = new DbContextOptionsBuilder<CommandDbContext>()
            .Options;

        return new CommandDbContext(options, Configuration);
    }

    private void MigrateDatabase()
    {
        using var dbContext = CreateCommandDbContext(); 
        dbContext.Database.Migrate();
    }

    public override async Task DisposeAsync()
    {
        if (_commandDbContext != null)
            await _commandDbContext.DisposeAsync();
        
        await base.DisposeAsync();
    }
}