using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Configurations.Converters;

public sealed class ReviewIdConverter : ValueConverter<ReviewId, Guid>
{
    public ReviewIdConverter()
        : base(id => id.Value, value => new ReviewId(value))
    {
    }
}