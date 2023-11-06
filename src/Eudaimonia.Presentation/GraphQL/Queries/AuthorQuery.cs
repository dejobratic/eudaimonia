using Eudaimonia.Application.Features.Books.GetAuthors;
using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Queries;
using Eudaimonia.Presentation.GraphQL.Types;
using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Queries;

public partial class RootQuery
{
    private void InitializeAuthorQuery()
    {
        Field<ListGraphType<AuthorType>>(name: "authors")
            .ResolveAsync(async context =>
            {
                var query = new GetAuthorsQuery();
                var dispatcher = (IQueryDispatcher)context.RequestServices!.GetService(typeof(IQueryDispatcher))!;
                return await dispatcher.DispatchAsync<IEnumerable<AuthorDto>>(query);
            });
    }
}