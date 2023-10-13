using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books.AddAuthor;

public class AddAuthorCommandAuthorFactory : IAuthorFactory<AddAuthorCommand>
{
    public Author CreateFrom(AddAuthorCommand command)
        => new(
            new Text(command.FullName!),
            string.IsNullOrEmpty(command.Bio) ? null : new Text(command.Bio!));
}