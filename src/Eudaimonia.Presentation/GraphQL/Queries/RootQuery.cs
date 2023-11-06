using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Queries
{
    public partial class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Name = "Query";
            InitializeAuthorQuery();
        }
    }
}