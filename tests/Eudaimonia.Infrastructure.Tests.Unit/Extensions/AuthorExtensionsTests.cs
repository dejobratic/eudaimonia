using Eudaimonia.Application.Dtos;
using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Extensions;

namespace Eudaimonia.Infrastructure.Tests.Unit.Extensions;

public class AuthorExtensionsTests
{
    [Fact]
    public void ToDto_GivenAuthor_ReturnsPublisherDto()
    {
        // Arrange
        var author = new Author(
            new Text("J.R.R. Tolkien"),
            new Text("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic."));

        // Act
        var actual = author.ToDto();

        // Assert
        var expected = new AuthorDto
        {
            Id = author.Id.Value,
            FullName = "J.R.R. Tolkien",
            Bio = "John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.",
        };

        Assert.Equivalent(expected, actual);
    }
}
