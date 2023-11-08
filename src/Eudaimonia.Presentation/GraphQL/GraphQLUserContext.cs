using System.Runtime.Serialization;
using System.Security.Claims;

namespace Eudaimonia.Presentation.GraphQL;

[Serializable]
public class GraphQLUserContext : Dictionary<string, object?>
{
    public ClaimsPrincipal User { get; set; } = null!;

    public GraphQLUserContext()
        : base()
    {
    }

    protected GraphQLUserContext(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}