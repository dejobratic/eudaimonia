using Eudaimonia.Application.Dtos;
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
        _builder.HasKey(p => p.Id);
        _builder.Property(p => p.FullName).IsRequired();
        _builder.Property(p => p.Bio);
        _builder.Ignore(p => p.PublishedEditions);
        _builder.HasMany(p => p.PublishedEditions).WithOne(e => e.Publisher).HasForeignKey(b => b.PublisherId).IsRequired();
    }
}