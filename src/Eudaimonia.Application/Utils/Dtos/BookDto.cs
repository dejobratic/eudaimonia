namespace Eudaimonia.Application.Utils.Dtos;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public virtual AuthorDto Author { get; set; } = null!;
    public EditionDto Edition { get; set; } = null!;
    public ReviewSummaryDto ReviewSummary { get; set; } = null!;
    public List<string> Genres { get; set; } = new();
}
