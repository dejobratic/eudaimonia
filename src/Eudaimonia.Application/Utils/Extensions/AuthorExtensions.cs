using Eudaimonia.Application.Dtos;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Utils.Extensions;

public static class AuthorExtensions
{
    public static AuthorDto ToDto(this Author author)
        => new()
        {
            Id = author.Id.Value,
            FullName = author.FullName.Value,
            Bio = author.Bio?.Value
        };
}