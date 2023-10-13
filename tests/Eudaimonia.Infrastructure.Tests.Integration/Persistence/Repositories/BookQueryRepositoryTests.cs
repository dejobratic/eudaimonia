using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Postgres;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Repositories;

public class BookQueryRepositoryTests : RepositoryTestBase
{
    private BookQueryRepository Sut => new(DbContext);

    public BookQueryRepositoryTests(PostgresFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task GetAllBooks_ReturnsAllExistingBooks()
    {
        // Arrange
        var author = new Author(
            new Text("J.R.R. Tolkien"),
            new Text("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic."));

        var publisher = new Publisher(
            new Text("HarperCollins"),
            new Text("HarperCollins Publishers LLC is one of the world's largest publishing companies."));

        // convert this to a Book entity
        var book1 = new Book(
            new Text("The Hobbit"),
            new Text("Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937."),
            author.Id,
            new Edition(
                310,
                new Image(new Text("Cover.jpg"), "https://pictures.abebooks.com/inventory/31499487055.jpg"),
                BookFormat.Hardcover,
                publisher.Id,
                new Year(1937)),
            new[] { Genre.Fantasy });

        var book2 = new Book(
            new Text("The Lord Of The Rings"),
            new Text("Set in Middle-earth, the story began as a sequel to Tolkien's 1937 children's book The Hobbit, but eventually developed into a much larger work."),
            author.Id,
            new Edition(
                975,
                new Image(new Text("Cover.jpg"), "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif"),
                BookFormat.Hardcover,
                publisher.Id,
                new Year(1968)),
            new[] { Genre.Fantasy });

        await AddAsync(author);
        await AddAsync(publisher);
        await AddAsync(book1);
        await AddAsync(book2);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetAllAsync();

        // Assert
        var expected = new[] { book1, book2 };

        Assert.Equivalent(expected, actual);
    }
}