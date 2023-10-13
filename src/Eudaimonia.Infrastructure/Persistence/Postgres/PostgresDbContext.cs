using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Persistence.Postgres;

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

        //modelBuilder.Entity<BookId>().HasNoKey();
    }
}
