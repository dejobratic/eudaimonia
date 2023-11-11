using Eudaimonia.Application.Dtos;
using Eudaimonia.Application.Utils.Commands;

namespace Eudaimonia.Application.Features.Books.AddBook;

public class AddBookCommand : ICommand
{
    public string? OriginalTitle { get; set; }
    public string? OriginalLanguage { get; set; }
    public Guid? AuthorId { get; set; }
    public EditionDto? Edition { get; set; }
    public IEnumerable<string> Genres { get; set; } = Array.Empty<string>();
}