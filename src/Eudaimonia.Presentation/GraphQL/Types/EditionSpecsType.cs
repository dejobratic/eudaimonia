using Eudaimonia.Application.Dtos;
using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Types;

public class EditionSpecsType : ObjectGraphType<EditionSpecsDto>
{
    public EditionSpecsType()
    {
        Name = "EditionSpecs";

        Field(_ => _.PageCount, nullable: true).Description("Edition's page count.");
        Field<ImageType>("FrontCover").Description("Edition's front cover.");
        Field(_ => _.Format).Description("Edition's format.");
    }
}