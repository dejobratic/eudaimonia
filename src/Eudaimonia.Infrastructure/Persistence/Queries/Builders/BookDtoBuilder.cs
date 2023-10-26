using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Infrastructure.Persistence.Queries.Builders.Comparers;
using Eudaimonia.Infrastructure.Persistence.Queries.Builders.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Builders;

public class BookDtoBuilder : IModelBuilder
{
    private readonly EntityTypeBuilder<BookDto> _builder;

    public BookDtoBuilder(EntityTypeBuilder<BookDto> builder)
    {
        _builder = builder;
    }

    public void Build()
    {
        _builder.ToTable(DbTableNames.Books);
        _builder.HasKey(b => b.Id);
        _builder.Property(b => b.Title).IsRequired();
        _builder.Property(b => b.Description).IsRequired();
        _builder.Property(b => b.AuthorId).IsRequired();
        _builder.HasOne(b => b.Author).WithMany(a => a.AuthoredBooks).HasForeignKey(b => b.AuthorId).IsRequired();
        _builder.OwnsOne(b => b.Edition, edition =>
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
        _builder.OwnsOne(b => b.ReviewSummary, reviewSummary =>
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
        _builder.Property(b => b.Genres).HasConversion(new GenresConverter()).Metadata.SetValueComparer(new GenresValueComparer());
    }
}