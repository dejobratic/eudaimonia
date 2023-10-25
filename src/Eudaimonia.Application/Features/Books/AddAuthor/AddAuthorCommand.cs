using Eudaimonia.Application.Utils.Commands;

namespace Eudaimonia.Application.Features.Books.AddAuthor;

public class AddAuthorCommand : ICommand
{
    public string? FullName { get; set; }
    public string? Bio { get; set; }
}