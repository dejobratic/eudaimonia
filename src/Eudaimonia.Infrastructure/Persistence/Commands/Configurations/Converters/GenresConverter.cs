using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Configurations.Converters;

public class GenresConverter : ValueConverter<IEnumerable<Genre>, string>
{
    private const char Delimiter = '|';

    public GenresConverter()
        : base(
            genres => string.Join(Delimiter, genres.Select(genre => genre.Name)),
            value => value.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries).Select(genre => Genre.FromName<Genre>(genre)).ToHashSet())
    {
    }
}
