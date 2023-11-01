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
            new BookId(),
            new Text("The Hobbit"),
            new Language("en"),
            authorId,
            new Edition(
                new EditionId(),
                new Text("The Hobbit"),
                new Text("Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937."),
                new Language("en"),
                new EditionSpecs(
                    310, 
                    new Image(new Text("Cover.jpg"), "https://pictures.abebooks.com/inventory/31499487055.jpg"), 
                    BookFormat.Hardcover),
                publisherId,
                new Year(1937)),
            new[] { Genre.Fantasy });

        // Act
        var actual = book.ToDto();

        // Assert
        var expected = new BookDto
        {
            Id = book.Id.Value,
            OriginalTitle = "The Hobbit",
            OriginalLanguage = "en",
            AuthorId = authorId.Value,
            DefaultEditionId = book.DefaultEditionId.Value,
            DefaultEdition = new EditionDto
            {
                Id = book.Editions.First().Id.Value,
                Title = "The Hobbit",
                Description = "Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.",
                Language = "en",
                Specs = new EditionSpecsDto
                {
                    PageCount = 310,
                    FrontCover = new ImageDto
                    {
                        Name = "Cover.jpg",
                        Url = "https://pictures.abebooks.com/inventory/31499487055.jpg"
                    },
                    Format = "Hardcover",
                },
                PublisherId = publisherId.Value,
                PublicationYear = 1937,
            },
            Genres = new List<string> { "Fantasy" },
        };

        Assert.Equivalent(expected, actual);
    }
}
