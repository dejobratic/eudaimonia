using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Postgres;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Repositories;

public class BookCommandRepositoryTests : RepositoryTestBase
{
    private BookCommandRepository Sut => new(DbContext);

    public BookCommandRepositoryTests(PostgresFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task AddBookAsync_ShouldAddBook()
    {
        // Arrange
        var author = new Author(
            new Text("J.R.R. Tolkien"),
            new Text("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic."));

        var publisher = new Publisher(
            new Text("HarperCollins"),
            new Text("HarperCollins Publishers LLC is one of the world's largest publishing companies."));

        await AddAsync(author);
        await AddAsync(publisher);
        await SaveChangesAsync();

        var book = new Book(
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

        // Act
        await Sut.AddAsync(book);

        // Assert
        var actual = await FindAsync<Book>(a => a.Id == book.Id);

        Assert.Equivalent(book, actual);
    }
}