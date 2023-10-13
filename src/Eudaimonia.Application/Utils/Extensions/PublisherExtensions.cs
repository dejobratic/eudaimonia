using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Utils.Extensions;

public static class PublisherExtensions
{
    public static PublisherDto ToDto(this Publisher publisher)
        => new()
        {
            Id = publisher.Id.Value,
            FullName = publisher.FullName.Value,
            Bio = publisher.Bio?.Value
        };
}