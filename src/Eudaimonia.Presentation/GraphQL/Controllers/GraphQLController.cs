using GraphQL;
using GraphQL.Transport;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Eudaimonia.Presentation.GraphQL.Controllers
{
    [Route("graphql")]
    public class GraphQLController : ControllerBase
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;

        public GraphQLController(
            ISchema schema,
            IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }

        [HttpPost]
        public async Task<IActionResult> GraphQLAsync([FromBody] GraphQLRequest request)
        {
            var result = await _documentExecuter.ExecuteAsync(s =>
            {
                s.Schema = _schema;
                s.Query = request.Query;
                s.Variables = request.Variables;
                s.OperationName = request.OperationName;
                s.RequestServices = HttpContext.RequestServices;
                s.UserContext = new GraphQLUserContext
                {
                    User = HttpContext.User,
                };
                s.CancellationToken = HttpContext.RequestAborted;
            });

            return new GraphQLActionResult(result);
        }
    }
}