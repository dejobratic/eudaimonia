using Eudaimonia.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eudaimonia.Infrastructure.Configurations;

public class PublisherConfiguration : IEntityTypeConfiguration<PublisherDto>
{
    public void Configure(EntityTypeBuilder<PublisherDto> builder)
    {
        builder.ToTable("Publishers");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.FullName).IsRequired();
        builder.Property(p => p.Bio);
        builder.Ignore(p => p.PublishedBookIds); //TODO: Link to books.
    }
}