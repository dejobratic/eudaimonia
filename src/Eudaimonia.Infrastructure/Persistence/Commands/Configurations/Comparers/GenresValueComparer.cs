using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Configurations.Comparers;

public class GenresValueComparer : ValueComparer<IEnumerable<Genre>>
{
    public GenresValueComparer()
        : base(
            (genres1, genres2) => genres1!.Order().SequenceEqual(genres2!.Order()),
            genres => genres.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            genres => genres)
    {
    }
}