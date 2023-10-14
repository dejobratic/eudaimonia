using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Extensions;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Tests.Unit.Extensions;

public class BookExtensionsTests
{
    [Fact]
    public void ToDto_GivenBook_ReturnsBookDto()
    {
        // Arrange
        var authorId = new AuthorId();
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
            ReviewSummary = new ReviewSummaryDto
            {
                ReviewCount = 0,
                RatingCount = 0,
                FiveStarRatingCount = 0,
                FourStarRatingCount = 0,
                ThreeStarRatingCount = 0,
                TwoStarRatingCount = 0,
                OneStarRatingCount = 0,
                AverageRating = 0,
            },
            Genres = new List<string> { "Fantasy" },
        };

        Assert.Equivalent(expected, actual);
    }
}
