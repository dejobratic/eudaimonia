using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Builders.Comparers;

public class GenresValueComparer : ValueComparer<List<string>>
{
    public GenresValueComparer()
        : base(
            (genres1, genres2) => genres1!.Order().SequenceEqual(genres2!.Order()),
            genres => genres.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            genres => genres)
    {
    }
}