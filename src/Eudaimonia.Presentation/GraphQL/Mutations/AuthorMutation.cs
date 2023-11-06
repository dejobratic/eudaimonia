using Eudaimonia.Application.Features.Books.AddAuthor;
using Eudaimonia.Presentation.GraphQL.Types;
using GraphQL;
using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Mutations;

public partial class RootMutation
{
    private void InitializeAuthorMutation()
    {
        Field<AuthorType>("addAuthor")
            .Argument<NonNullGraphType<AddAuthorType>>("author")
            .ResolveAsync(async context =>
            {
                var command = context.GetArgument<AddAuthorCommand>("author");
                return await _commandDispatcher.DispatchAsync(command);
            });
    }
}