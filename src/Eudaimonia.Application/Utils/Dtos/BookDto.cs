namespace Eudaimonia.Application.Utils.Dtos;

public class BookDto
{
    public Guid Id { get; set; }
    public string OriginalTitle { get; set; } = null!;
    public string OriginalLanguage { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public virtual AuthorDto Author { get; set; } = null!;
    public Guid DefaultEditionId { get; set; }
    public virtual EditionDto DefaultEdition { get; set; } = null!;
    public List<string> Genres { get; set; } = new();
}
