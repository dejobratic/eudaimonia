using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Configurations.Converters;

public sealed class CommentIdConverter : ValueConverter<CommentId, Guid>
{
    public CommentIdConverter()
        : base(id => id.Value, value => new CommentId(value))
    {
    }
}