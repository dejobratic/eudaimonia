using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eudaimonia.Infrastructure.Configurations;

public sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasConversion<ReviewIdConverter>().IsRequired();
        builder.Property(r => r.BookId).HasConversion<BookIdConverter>().IsRequired();
        builder.Property(r => r.ReviewerId).HasConversion<UserIdConverter>().IsRequired();
        builder.Property(r => r.Rating).HasConversion<RatingConverter>().IsRequired();
        builder.OwnsOne(r => r.Comment, ConfigureComment);
    }

    private static void ConfigureComment(OwnedNavigationBuilder<Review, Comment> builder)
    {
        builder.WithOwner().HasForeignKey("ReviewId");
        builder.ToTable("Comments");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion<CommentIdConverter>().IsRequired();
        builder.Property(c => c.CommenterId).HasConversion<UserIdConverter>().IsRequired();
        builder.Property(c => c.Text).HasConversion<TextConverter>().IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();
    }
}