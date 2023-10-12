using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain;

public class Rating : Enumeration<byte>
{
    public static readonly Rating OneStar = new("1", 1);
    public static readonly Rating TwoStar = new("2", 2);
    public static readonly Rating ThreeStar = new("3", 3);
    public static readonly Rating FourStar = new("4", 4);
    public static readonly Rating FiveStar = new("5", 5);

    private Rating(string name, byte value) : base(name, value)
    {
    }
}