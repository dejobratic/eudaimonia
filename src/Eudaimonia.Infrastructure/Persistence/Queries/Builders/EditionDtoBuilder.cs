using Eudaimonia.Application.Utils.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Builders;

public class EditionDtoBuilder : IModelBuilder
{
    private readonly EntityTypeBuilder<EditionDto> _builder;

    public EditionDtoBuilder(EntityTypeBuilder<EditionDto> builder)
    {
        _builder = builder;
    }

    public void Build()
    {
        _builder.ToTable(DbTableNames.Editions);
        _builder.HasKey(e => e.Id);
        _builder.Property(e => e.Title).IsRequired();
        _builder.Property(e => e.Description).IsRequired();
        _builder.Property(e => e.Language).IsRequired();
        _builder.OwnsOne(e => e.Specs, ConfigureEditionSpecs);
        _builder.Property(e => e.BookId).IsRequired();
        _builder.HasOne(e => e.Book).WithOne(e => e.DefaultEdition).HasForeignKey(nameof(EditionDto), nameof(EditionDto.BookId));
        _builder.Property(e => e.PublisherId).IsRequired();
        _builder.HasOne(e => e.Publisher).WithMany(p => p.PublishedEditions).HasForeignKey(e => e.PublisherId);
        _builder.Property(e => e.PublicationYear).IsRequired();
    }

    private static void ConfigureEditionSpecs(OwnedNavigationBuilder<EditionDto, EditionSpecsDto> builder)
    {
        builder.WithOwner();
        builder.Property(s => s.PageCount).IsRequired();
        builder.Property(s => s.Format).IsRequired();
        builder.OwnsOne(s => s.FrontCover, ConfigureFrontCover);
    }

    private static void ConfigureFrontCover(OwnedNavigationBuilder<EditionSpecsDto, ImageDto> builder)
    {
        builder.WithOwner();
        builder.Property(fc => fc.Name).IsRequired();
        builder.Property(fc => fc.Url).IsRequired();
    }
}