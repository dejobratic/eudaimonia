using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eudaimonia.Infrastructure.Configurations;

public sealed class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.ToTable("Publishers");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion<PublisherIdConverter>().IsRequired();
        builder.Property(p => p.FullName).HasConversion<TextConverter>().IsRequired();
        builder.Property(p => p.Bio).HasConversion<TextConverter>();
    }
}