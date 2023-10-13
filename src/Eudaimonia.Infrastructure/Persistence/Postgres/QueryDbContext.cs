using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Persistence.Postgres;

// TODO: figure out a way to make this work with DTOs directly
public sealed class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions options)
        : base(options)
    {
    }

    // Probably not required, since https://stackoverflow.com/questions/10437058/how-to-make-entity-framework-data-context-readonly
    // Don't rely on this, because (context as IObjectContextAdapter).ObjectContext.SaveChanges() will still work.
    // The best choice is to use the DbContext(string nameOrConnectionString); contstructor with a read/write connectionstring for database creation stuff and a readonly connection string afterwards. 
    public override int SaveChanges()
        => throw new InvalidOperationException("This context is read-only.");

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
        => SaveChanges();

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        => Task.FromResult(SaveChanges());

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(SaveChanges());
}
