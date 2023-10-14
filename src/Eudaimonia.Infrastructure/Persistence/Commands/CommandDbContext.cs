using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Persistence.Commands;

public class CommandDbContext : DbContext
{
    public CommandDbContext(DbContextOptions<CommandDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommandDbContext).Assembly);
    }
}