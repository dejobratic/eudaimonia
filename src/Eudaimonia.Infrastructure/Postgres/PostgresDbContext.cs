using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Postgres;

public class PostgresDbContext : DbContext
{
    public PostgresDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly);
    }
}
