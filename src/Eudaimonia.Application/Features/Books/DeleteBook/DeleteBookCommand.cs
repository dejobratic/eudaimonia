using Eudaimonia.Application.Utils.Commands;

namespace Eudaimonia.Application.Features.Books.DeleteBook;

public class DeleteBookCommand : ICommand
{
    public Guid Id { get; set; }
}