using Eudaimonia.Domain;
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
        builder.Property(b => b.OriginalTitle).HasConversion<TextConverter>().IsRequired();
        builder.Property(b => b.OriginalLanguage).HasConversion<LanguageConverter>().IsRequired();
        builder.HasOne<Author>().WithMany().HasForeignKey(b => b.AuthorId);
        builder.Property(b => b.AuthorId).HasConversion<AuthorIdConverter>().IsRequired();
        builder.Property(b => b.DefaultEditionId).HasConversion<EditionIdConverter>().IsRequired();
        builder.OwnsMany(b => b.Editions, ConfigureEditions);
        builder.Property(b => b.Genres).HasConversion<GenresConverter>().Metadata.SetValueComparer(new GenresValueComparer());
    }

    private static void ConfigureEditions(OwnedNavigationBuilder<Book, Edition> builder)
    {
        builder.WithOwner().HasForeignKey("BookId");
        builder.ToTable(DbTableNames.Editions);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasConversion<EditionIdConverter>().IsRequired();
        builder.Property(e => e.Title).HasConversion<TextConverter>().IsRequired();
        builder.Property(e => e.Description).HasConversion<TextConverter>().IsRequired();
        builder.Property(e => e.Language).HasConversion<LanguageConverter>().IsRequired();
        builder.OwnsOne(e => e.Specs, ConfigureEditionSpecs);
        builder.HasOne<Publisher>().WithMany().HasForeignKey(e => e.PublisherId);
        builder.Property(e => e.PublisherId).HasConversion<PublisherIdConverter>().IsRequired();
        builder.Property(e => e.PublicationYear).HasConversion<YearConverter>().IsRequired();
    }

    private static void ConfigureEditionSpecs(OwnedNavigationBuilder<Edition, EditionSpecs> builder)
    {
        builder.WithOwner();
        builder.Property(e => e.PageCount).IsRequired();
        builder.Property(e => e.Format).HasConversion<EnumToStringConverter<BookFormat>>().IsRequired();
        builder.OwnsOne(e => e.FrontCover, ConfigureFrontCover);
    }

    private static void ConfigureFrontCover(OwnedNavigationBuilder<EditionSpecs, Image> builder)
    {
        builder.WithOwner();
        builder.Property(fc => fc.Name).HasConversion<TextConverter>().IsRequired();
        builder.Property(fc => fc.Url).IsRequired();
    }
}