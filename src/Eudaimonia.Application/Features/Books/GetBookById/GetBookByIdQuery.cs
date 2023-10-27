using Eudaimonia.Application.Utils.Queries;

namespace Eudaimonia.Application.Features.Books.GetBookById;

public class GetBookByIdQuery : IQuery
{
    public Guid Id { get; init; }
}