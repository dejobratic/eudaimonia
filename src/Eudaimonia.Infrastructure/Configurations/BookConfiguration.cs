using Eudaimonia.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<BookDto>
{
    private sealed class GenresConversion : ValueConverter<IEnumerable<string>, string>
    {
        public GenresConversion()
            : base(genres => string.Join(",", genres), value => value.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(g => g))
        {

        }
    }

    private sealed class GenresValueComparer : ValueComparer<IEnumerable<string>>
    {
        public GenresValueComparer()
            : base((c1, c2) => c1!.SequenceEqual(c2!), c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => c.ToList())
        {

        }
    }

    public void Configure(EntityTypeBuilder<BookDto> builder)
    {
        builder.ToTable("Books");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedNever();
        builder.Property(b => b.Title).IsRequired();
        builder.Property(b => b.Description).IsRequired();
        builder.HasOne<AuthorDto>().WithMany().HasForeignKey(b => b.AuthorId);
        builder.OwnsOne(b => b.Edition, editionBuilder =>
        {
            editionBuilder.WithOwner();
            editionBuilder.Property(e => e.PageCount).IsRequired();
            editionBuilder.Property(e => e.Format).IsRequired();
            editionBuilder.OwnsOne(e => e.FrontCover, frontCoverBuilder =>
            {
                frontCoverBuilder.WithOwner();
                frontCoverBuilder.Property(fc => fc.Name).IsRequired();
                frontCoverBuilder.Property(fc => fc.Url).IsRequired();
            });
            editionBuilder.Property(e => e.PublisherId).IsRequired();
            editionBuilder.Property(e => e.PublicationYear).IsRequired();
        });
        builder.OwnsOne(b => b.ReviewSummary, reviewSummaryBuilder =>
        {
            reviewSummaryBuilder.WithOwner();
            reviewSummaryBuilder.Property(rs => rs.ReviewCount).IsRequired();
            reviewSummaryBuilder.Property(rs => rs.RatingCount).IsRequired();
            reviewSummaryBuilder.Property(rs => rs.FiveStarRatingCount).IsRequired();
            reviewSummaryBuilder.Property(rs => rs.FourStarRatingCount).IsRequired();
            reviewSummaryBuilder.Property(rs => rs.ThreeStarRatingCount).IsRequired();
            reviewSummaryBuilder.Property(rs => rs.TwoStarRatingCount).IsRequired();
            reviewSummaryBuilder.Property(rs => rs.OneStarRatingCount).IsRequired();
            reviewSummaryBuilder.Property(rs => rs.AverageRating).IsRequired();
        });
        builder.Property(b => b.Genres).HasConversion(new GenresConversion());
        builder.Property(b => b.Genres).Metadata.SetValueComparer(new GenresValueComparer());
    }
}