using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain;

public sealed class Genre : Enumeration<string>
{
    public static readonly Genre Fantasy = new("Fantasy", "Fantasy");
    public static readonly Genre ScienceFiction = new("Science Fiction", "Science Fiction");

    private Genre(string name, string value) : base(name, value)
    {
    }
}