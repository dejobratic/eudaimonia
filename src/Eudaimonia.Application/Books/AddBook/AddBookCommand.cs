using Eudaimonia.Application.Dtos;

namespace Eudaimonia.Application.Books.AddBook;

public class AddBookCommand : ICommand
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Guid? AuthorId { get; set; }
    public EditionDto? Edition { get; set; }
    public IEnumerable<string> Genres { get; set; } = Array.Empty<string>();
}
