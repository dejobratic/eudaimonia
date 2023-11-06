using Eudaimonia.Application.Utils.Dtos;
using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Types;

public class PublisherType : ObjectGraphType<PublisherDto>
{
    public PublisherType()
    {
        Name = "Publisher";

        Field(_ => _.Id).Description("Publisher's id.");
        Field(_ => _.FullName).Description("Publisher's full name.");
        Field(_ => _.Bio).Description("Publisher's bio.");
        Field<ListGraphType<EditionType>>(nameof(PublisherDto.PublishedEditions));
    }
}