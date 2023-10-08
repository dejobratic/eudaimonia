using Eudaimonia.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eudaimonia.Infrastructure.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<AuthorDto>
{
    public void Configure(EntityTypeBuilder<AuthorDto> builder)
    {
        builder.ToTable("Authors");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FullName).IsRequired();
        builder.Property(u => u.Bio);
        builder.HasMany<BookDto>().WithOne().HasForeignKey(b => b.AuthorId).IsRequired();
    }
}