using Eudaimonia.Application.Dtos;
using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Postgres.Repositories;

namespace Eudaimonia.Infrastructure.Tests.Integration.Postgres.Repositories;

public class BookCommandRepositoryTests : TestBase
{
    private BookCommandRepository Sut => new(DbContext);

    [Fact]
    public async Task AddBookAsync_ShouldAddBook()
    {
        // Arrange
        var author = new AuthorDto
        {
            Id = Guid.NewGuid(),
            FullName = "J.R.R. Tolkien",
            Bio = "John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.",
        };

        var publisher = new PublisherDto
        {
            Id = Guid.NewGuid(),
            FullName = "HarperCollins",
            Bio = "HarperCollins Publishers LLC is one of the world's largest publishing companies.",
        };

        await AddAsync(author);
        await AddAsync(publisher);
        await SaveChangesAsync();

        var book = new Book(
            new Text("The Hobbit"),
            new Text("Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937."),
            new UserId(author.Id),
            new Edition(310, new Image(new Text("Cover.jpg"), "https://pictures.abebooks.com/inventory/31499487055.jpg"), BookFormat.Hardcover, new PublisherId(publisher.Id), new Year(1937)),
            new[] { Genre.Fantasy });

        // Act
        await Sut.AddAsync(book);

        // Assert
        var actual = await FindAsync<BookDto>(a => a.Id == book.Id.Value);

        var expected = new BookDto
        {
            Id = book.Id.Value,
            Title = "The Hobbit",
            Description = "Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.",
            AuthorId = author.Id,
            Edition = new EditionDto
            {
                PageCount = 310,
                FrontCover = new ImageDto
                {
                    Name = "Cover.jpg",
                    Url = "https://pictures.abebooks.com/inventory/31499487055.jpg"
                },
                Format = "Hardcover",
                PublisherId = publisher.Id,
                PublicationYear = 1937,
            },
            ReviewSummary = new ReviewSummaryDto(),
            Genres = new[] { "Fantasy" },
        };

        Assert.Equivalent(expected, actual);
    }
}