namespace Eudaimonia.Application.Dtos;

public class PublisherDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? Bio { get; set; }

    private readonly List<Guid> _publishedBookIds = new();

    public IReadOnlyList<Guid> PublishedBookIds => _publishedBookIds;
}
