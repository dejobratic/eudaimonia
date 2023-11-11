using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Configurations.Comparers;

public class GenresValueComparer : ValueComparer<IEnumerable<Genre>>
{
    public GenresValueComparer()
        : base(
            (genres1, genres2) => Compare(genres1, genres2),
            genres => genres.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            genres => genres)
    {
    }

    private static bool Compare(IEnumerable<Genre>? genres1, IEnumerable<Genre>? genres2)
    {
        if (genres1 is null || genres2 is null)
            return false;

        if (ReferenceEquals(genres1, genres2))
            return true;

        return genres1.Order().SequenceEqual(genres2.Order());
    }
}