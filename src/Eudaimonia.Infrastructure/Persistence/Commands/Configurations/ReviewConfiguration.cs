using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Commands.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Configurations;

public sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable(DbTableNames.Reviews);
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasConversion<ReviewIdConverter>().IsRequired();
        builder.Property(r => r.BookId).HasConversion<BookIdConverter>().IsRequired();
        builder.Property(r => r.ReviewerId).HasConversion<UserIdConverter>().IsRequired();
        builder.Property(r => r.Rating).HasConversion<RatingConverter>().IsRequired();
        builder.Property(r => r.Comment).HasConversion<TextConverter>();
    }
}