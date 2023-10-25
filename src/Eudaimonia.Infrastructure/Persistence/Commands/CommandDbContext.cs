using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eudaimonia.Infrastructure.Persistence.Commands;

public class CommandDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public CommandDbContext(
        DbContextOptions<CommandDbContext> options,
        IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(_configuration[DbOptions.PostgresConnectionString]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommandDbContext).Assembly);
    }
}