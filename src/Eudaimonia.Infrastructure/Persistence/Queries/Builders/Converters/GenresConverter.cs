using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Builders.Converters;

public class GenresConverter : ValueConverter<List<string>, string>
{
    private const char Delimiter = '|';

    public GenresConverter()
        : base(
            genres => string.Join(Delimiter, genres.Select(genre => genre)),
            value => value.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries).ToList())
    {
    }
}
