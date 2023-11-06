using Eudaimonia.Application.Utils.Commands;
using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Mutations;

public partial class RootMutation : ObjectGraphType
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RootMutation(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;

        Name = "Mutation";
        InitializeAuthorMutation();
    }
}