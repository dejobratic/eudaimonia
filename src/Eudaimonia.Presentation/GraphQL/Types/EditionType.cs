using Eudaimonia.Application.Utils.Dtos;
using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Types;

public class EditionType : ObjectGraphType<EditionDto>
{
    public EditionType()
    {
        Name = "Edition";

        Field(_ => _.Id, nullable: true).Description("Edition's id.");
        Field(_ => _.Title).Description("Edition's title.");
        Field(_ => _.Description).Description("Edition's description.");
        Field(_ => _.Language).Description("Edition's language.");
        Field<EditionSpecsType>(nameof(EditionDto.Specs)).Description("Edition's specs.");
        Field(_ => _.BookId, nullable: true).Description("Edition's book id.");
        Field<BookType>(nameof(EditionDto.Book)).Description("Edition's book.");
        Field(_ => _.PublisherId, nullable: true).Description("Edition's publisher id.");
        Field<PublisherType>(nameof(EditionDto.Publisher)).Description("Edition's publisher.");
        Field(_ => _.PublicationYear, nullable: true).Description("Edition's publication year.");
    }
}