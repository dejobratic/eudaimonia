using Eudaimonia.Presentation.GraphQL.Mutations;
using Eudaimonia.Presentation.GraphQL.Queries;
using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Schemas;

public class RootSchema : Schema
{
    public RootSchema(RootQuery query, RootMutation mutation)
    {
        Query = query;
        Mutation = mutation;
    }
}