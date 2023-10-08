using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain;

public sealed class Image : ValueObject<Image>
{
    public Text Name { get; }
    public string Url { get; }

    public Image(Text name, string url)
    {
        Name = name;
        Url = url;
    }
}