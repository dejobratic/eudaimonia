using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Configurations.Converters;

public sealed class EditionIdConverter : ValueConverter<EditionId, Guid>
{
    public EditionIdConverter()
        : base(id => id.Value, value => new EditionId(value))
    {
    }
}