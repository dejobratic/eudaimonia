using Eudaimonia.Application.Utils;

namespace Eudaimonia.Application.Features.Books.AddAuthor;

public class AddAuthorCommand : ICommand
{
    public string? FullName { get; set; }
    public string? Bio { get; set; }
}