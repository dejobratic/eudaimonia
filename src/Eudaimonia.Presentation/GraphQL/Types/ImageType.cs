using Eudaimonia.Application.Dtos;
using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Types;

public class ImageType : ObjectGraphType<ImageDto>
{
    public ImageType()
    {
        Name = "Image";

        Field(_ => _.Name).Description("Image's name.");
        Field(_ => _.Url).Description("Image's url.");
    }
}