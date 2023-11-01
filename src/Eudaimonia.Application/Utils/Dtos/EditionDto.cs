namespace Eudaimonia.Application.Utils.Dtos;

public class EditionDto
{
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Language { get; set; }
    public EditionSpecsDto? Specs { get; set; }
    public Guid? BookId { get; set; }
    public virtual BookDto? Book { get; set; } = null!;
    public Guid? PublisherId { get; set; }
    public virtual PublisherDto? Publisher { get; set; } = null!;
    public int? PublicationYear { get; set; }
}