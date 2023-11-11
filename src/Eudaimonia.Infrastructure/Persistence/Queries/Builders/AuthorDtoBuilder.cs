using Eudaimonia.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Builders;

public class AuthorDtoBuilder : IModelBuilder
{
    private readonly EntityTypeBuilder<AuthorDto> _builder;

    public AuthorDtoBuilder(EntityTypeBuilder<AuthorDto> builder)
    {
        _builder = builder;
    }

    public void Build()
    {
        _builder.ToTable(DbTableNames.Authors);
        _builder.HasKey(a => a.Id);
        _builder.Property(a => a.FullName).IsRequired();
        _builder.Property(a => a.Bio);
        _builder.HasMany(a => a.AuthoredBooks).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId);
    }
}