using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Infrastructure.Persistence.Queries.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eudaimonia.Infrastructure.Persistence.Queries;

public sealed class QueryDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public QueryDbContext(
        DbContextOptions<QueryDbContext> options,
        IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(_configuration[DbOptions.PostgresConnectionString]);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AuthorDto>(b => new AuthorDtoBuilder(b).Build());
        modelBuilder.Entity<PublisherDto>(b => new PublisherDtoBuilder(b).Build());
        modelBuilder.Entity<BookDto>(b => new BookDtoBuilder(b).Build());
    }
}