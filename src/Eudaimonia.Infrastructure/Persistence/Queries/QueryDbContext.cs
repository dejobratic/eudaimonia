using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Infrastructure.Persistence.Queries.Configurations.Comparers;
using Eudaimonia.Infrastructure.Persistence.Queries.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        modelBuilder.Entity<AuthorDto>(ConfigureAuthorDto);

        modelBuilder.Entity<PublisherDto>(ConfigurePublisherDto);

        modelBuilder.Entity<BookDto>(ConfigureBookDto);
    }

    private static void ConfigureAuthorDto(EntityTypeBuilder<AuthorDto> builder)
    {
        builder.ToTable(DbTableNames.Authors);
        builder.HasKey(a => a.Id);
        builder.Property(a => a.FullName).IsRequired();
        builder.Property(a => a.Bio);
        builder.HasMany(a => a.AuthoredBooks).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId).IsRequired();
    }

    private static void ConfigurePublisherDto(EntityTypeBuilder<PublisherDto> builder)
    {
        builder.ToTable(DbTableNames.Publishers);
        builder.HasKey(a => a.Id);
        builder.Property(a => a.FullName).IsRequired();
        builder.Property(a => a.Bio);
        builder.Ignore(a => a.PublishedBooks); // TODO: add link to books
    }

    private static void ConfigureBookDto(EntityTypeBuilder<BookDto> builder)
    {
        builder.ToTable(DbTableNames.Books);
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired();
        builder.Property(b => b.Description).IsRequired();
        builder.Property(b => b.AuthorId).IsRequired();
        builder.HasOne(b => b.Author).WithMany(a => a.AuthoredBooks).HasForeignKey(b => b.AuthorId).IsRequired();
        builder.OwnsOne(b => b.Edition, edition =>
        {
            edition.WithOwner();
            edition.Property(e => e.PageCount).IsRequired();
            edition.Property(e => e.Format).IsRequired();
            edition.OwnsOne(e => e.FrontCover, frontCover =>
            {
                frontCover.WithOwner();
                frontCover.Property(fc => fc.Name).IsRequired();
                frontCover.Property(fc => fc.Url).IsRequired();
            });
            edition.Property(e => e.PublisherId).IsRequired();
            edition.Property(e => e.PublicationYear).IsRequired();
        });
        builder.OwnsOne(b => b.ReviewSummary, reviewSummary =>
        {
            reviewSummary.WithOwner();
            reviewSummary.Property(rs => rs.ReviewCount).IsRequired();
            reviewSummary.Property(rs => rs.RatingCount).IsRequired();
            reviewSummary.Property(rs => rs.FiveStarRatingCount).IsRequired();
            reviewSummary.Property(rs => rs.FourStarRatingCount).IsRequired();
            reviewSummary.Property(rs => rs.ThreeStarRatingCount).IsRequired();
            reviewSummary.Property(rs => rs.TwoStarRatingCount).IsRequired();
            reviewSummary.Property(rs => rs.OneStarRatingCount).IsRequired();
            reviewSummary.Property(rs => rs.AverageRating).IsRequired();
        });
        builder.Property(b => b.Genres).HasConversion(new GenresConverter()).Metadata.SetValueComparer(new GenresValueComparer());
    }
}