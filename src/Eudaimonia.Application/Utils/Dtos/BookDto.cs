namespace Eudaimonia.Application.Utils.Dtos;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public EditionDto Edition { get; set; } = null!;
    public ReviewSummaryDto ReviewSummary { get; set; } = null!;
    public IEnumerable<string> Genres { get; set; } = Array.Empty<string>();
}
