namespace Eudaimonia.Application.Dtos;

public class PublisherDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? Bio { get; set; }

    public virtual List<EditionDto> PublishedEditions { get; set; } = new();
}