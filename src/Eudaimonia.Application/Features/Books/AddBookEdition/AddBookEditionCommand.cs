using Eudaimonia.Application.Dtos;
using Eudaimonia.Application.Utils.Commands;

namespace Eudaimonia.Application.Features.Books.AddBookEdition;

public class AddBookEditionCommand : ICommand
{
    public Guid BookId { get; set; }
    public EditionDto? Edition { get; set; }
}