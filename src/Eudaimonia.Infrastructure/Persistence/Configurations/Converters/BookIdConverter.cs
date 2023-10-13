using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Persistence.Configurations.Converters;

public sealed class BookIdConverter : ValueConverter<BookId, Guid>
{
    public BookIdConverter()
        : base(id => id.Value, value => new BookId(value))
    {
    }
}