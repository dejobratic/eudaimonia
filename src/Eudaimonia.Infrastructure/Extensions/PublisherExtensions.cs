using Eudaimonia.Application.Dtos;
using Eudaimonia.Domain;

namespace Eudaimonia.Infrastructure.Extensions;

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