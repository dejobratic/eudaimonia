using Eudaimonia.Application.Dtos;
using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Types;

public class AuthorType : ObjectGraphType<AuthorDto>
{
    public AuthorType()
    {
        Name = "Author";

        Field(_ => _.Id).Description("Author's id.");
        Field(_ => _.FullName).Description("Author's full name.");
        Field(_ => _.Bio).Description("Author's bio.");
        Field<ListGraphType<BookType>>(nameof(AuthorDto.AuthoredBooks));
    }
}