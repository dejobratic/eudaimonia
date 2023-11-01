using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Extensions;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Tests.Unit.Extensions;

public class AuthorExtensionsTests
{
    [Fact]
    public void ToDto_GivenAuthor_ReturnsPublisherDto()
    {
        // Arrange
        var author = new Author(
            new AuthorId(),
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
