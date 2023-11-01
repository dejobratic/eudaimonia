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
        _builder.Property(b => b.OriginalTitle).IsRequired();
        _builder.Property(b => b.OriginalLanguage).IsRequired();
        _builder.Property(b => b.AuthorId).IsRequired();
        _builder.HasOne(b => b.Author).WithMany(a => a.AuthoredBooks).HasForeignKey(b => b.AuthorId);
        _builder.HasOne(b => b.DefaultEdition).WithOne(e => e.Book).HasForeignKey(nameof(BookDto), nameof(BookDto.DefaultEditionId));
        _builder.Property(b => b.Genres).HasConversion(new GenresConverter()).Metadata.SetValueComparer(new GenresValueComparer());
    }
}