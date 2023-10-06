using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain;

public sealed class Image : ValueObject<Image>
{
    public Text Name { get; }
    public string? Url { get; }
    public byte[] Data { get; } = Array.Empty<byte>();
    public bool HasData => Data.Any();

    public Image(Text name, string url)
    {
        Name = name;
        Url = url;
    }

    public Image(Text name, byte[] data)
    {
        Name = name;
        Data = data;
    }
}