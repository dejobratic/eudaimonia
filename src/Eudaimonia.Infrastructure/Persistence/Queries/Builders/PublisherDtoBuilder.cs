using Eudaimonia.Application.Utils.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Builders;

public class PublisherDtoBuilder : IModelBuilder
{
    private readonly EntityTypeBuilder<PublisherDto> _builder;

    public PublisherDtoBuilder(EntityTypeBuilder<PublisherDto> builder)
    {
        _builder = builder;
    }

    public void Build()
    {
        _builder.ToTable(DbTableNames.Publishers);
        _builder.HasKey(a => a.Id);
        _builder.Property(a => a.FullName).IsRequired();
        _builder.Property(a => a.Bio);
        _builder.Ignore(a => a.PublishedBooks); // TODO: add link to books
    }
}