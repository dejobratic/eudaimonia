﻿using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Commands.Configurations.Comparers;
using Eudaimonia.Infrastructure.Persistence.Commands.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Configurations;

public sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable(DbTableNames.Books);
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasConversion<BookIdConverter>().IsRequired();
        builder.Property(b => b.Title).HasConversion<TextConverter>().IsRequired();
        builder.Property(b => b.Description).HasConversion<TextConverter>().IsRequired();
        builder.HasOne<Author>().WithMany().HasForeignKey(b => b.AuthorId);
        builder.Property(b => b.AuthorId).HasConversion<AuthorIdConverter>().IsRequired();
        builder.OwnsOne(b => b.Edition, ConfigureEdition);
        builder.OwnsOne(b => b.ReviewSummary, ConfigureReviewSummary);
        builder.Property(b => b.Genres).HasConversion<GenresConverter>().Metadata.SetValueComparer(new GenresValueComparer());
    }

    private static void ConfigureEdition(OwnedNavigationBuilder<Book, Edition> builder)
    {
        builder.WithOwner();
        builder.Property(e => e.PageCount).IsRequired();
        builder.Property(e => e.Format).HasConversion<EnumToStringConverter<BookFormat>>().IsRequired();
        builder.OwnsOne(e => e.FrontCover, ConfigureFrontCover);
        builder.Property(e => e.PublisherId).HasConversion<PublisherIdConverter>().IsRequired();
        builder.Property(e => e.PublicationYear).HasConversion<YearConverter>().IsRequired();
    }

    private static void ConfigureFrontCover(OwnedNavigationBuilder<Edition, Image> builder)
    {
        builder.WithOwner();
        builder.Property(fc => fc.Name).HasConversion<TextConverter>().IsRequired();
        builder.Property(fc => fc.Url).IsRequired();
    }

    private static void ConfigureReviewSummary(OwnedNavigationBuilder<Book, ReviewSummary> builder)
    {
        builder.WithOwner();
        builder.Property(rs => rs.ReviewCount).IsRequired();
        builder.Property(rs => rs.RatingCount).IsRequired();
        builder.Property(rs => rs.FiveStarRatingCount).IsRequired();
        builder.Property(rs => rs.FourStarRatingCount).IsRequired();
        builder.Property(rs => rs.ThreeStarRatingCount).IsRequired();
        builder.Property(rs => rs.TwoStarRatingCount).IsRequired();
        builder.Property(rs => rs.OneStarRatingCount).IsRequired();
        builder.Property(rs => rs.AverageRating).IsRequired();
    }
}