using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Commands.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Configurations;

public sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable(DbTableNames.Authors);
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion<AuthorIdConverter>().IsRequired();
        builder.Property(a => a.FullName).HasConversion<TextConverter>().IsRequired();
        builder.Property(a => a.Bio).HasConversion<TextConverter>();
    }
}