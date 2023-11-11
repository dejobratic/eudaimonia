using Eudaimonia.Application.Features.Authors.AddAuthor;
using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Types;

public class AddAuthorType : InputObjectGraphType<AddAuthorCommand>
{
    public AddAuthorType()
    {
        Name = "AddAuthor";

        Field(_ => _.FullName).Description("Author's full name.");
        Field(_ => _.Bio).Description("Author's bio.");
    }
}