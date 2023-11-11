using Eudaimonia.Application.Utils.Commands;

namespace Eudaimonia.Application.Features.Authors.DeleteAuthor;

public class DeleteAuthorCommand : ICommand
{
    public Guid Id { get; set; }
}