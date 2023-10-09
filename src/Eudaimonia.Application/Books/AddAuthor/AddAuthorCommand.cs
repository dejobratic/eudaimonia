namespace Eudaimonia.Application.Books.AddAuthor;

public class AddAuthorCommand : ICommand
{
    public string? FullName { get; set; }
    public string? Bio { get; set; }
}