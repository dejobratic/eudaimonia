using Eudaimonia.Application.Dtos;
using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Extensions;

namespace Eudaimonia.Infrastructure.Tests.Unit.Extensions;

public class BookExtensionsTests
{
    [Fact]
    public void ToDto_GivenBook_ReturnsBookDto()
    {
        // Arrange
        var authorId = new UserId();
        var publisherId = new PublisherId();

        var book = new Book(
            new Text("The Hobbit"),
            new Text("Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937."),
            authorId,
            new Edition(310, new Image(new Text("Cover.jpg"), "https://pictures.abebooks.com/inventory/31499487055.jpg"), BookFormat.Hardcover, publisherId, new Year(1937)),
            new[] { Genre.Fantasy });

        // Act
        var actual = book.ToDto();

        // Assert
        var expected = new BookDto
        {
            Id = book.Id.Value,
            Title = "The Hobbit",
            Description = "Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.",
            AuthorId = authorId.Value,
            Edition = new EditionDto
            {
                PageCount = 310,
                FrontCover = new ImageDto
                {
                    Name = "Cover.jpg",
                    Url = "https://pictures.abebooks.com/inventory/31499487055.jpg"
                },
                Format = "Hardcover",
                PublisherId = publisherId.Value,
                PublicationYear = 1937,
            },
            ReviewSummary = new ReviewSummaryDto(),
            Genres = new[] { "Fantasy" },
        };

        Assert.Equivalent(expected, actual);
    }
}
