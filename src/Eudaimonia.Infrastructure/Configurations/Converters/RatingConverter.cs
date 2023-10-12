using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Configurations.Converters;

public sealed class RatingConverter : ValueConverter<Rating, byte>
{
    public RatingConverter()
        : base(rating => rating.Value, value => Rating.FromValue<Rating>(value))
    {
    }
}