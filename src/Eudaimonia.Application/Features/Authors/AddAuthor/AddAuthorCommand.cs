using Eudaimonia.Application.Utils.Commands;

namespace Eudaimonia.Application.Features.Authors.AddAuthor;

public class AddAuthorCommand : ICommand
{
    public string? FullName { get; set; }
    public string? Bio { get; set; }
}