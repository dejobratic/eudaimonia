using Eudaimonia.Domain.Exceptions;
using Eudaimonia.Infrastructure.Persistence.Queries.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Builders;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Repositories;

public class BookDtoRepositoryTests(QueryDbFixture fixture) : QueryDbTestsBase(fixture)
{
    private BookDtoRepository Sut => new(DbContext);

    [Fact]
    public async Task GetById_WhenBookDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        Task action() => Sut.GetByIdAsync(id);

        // Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(action);
        Assert.Equal($"Book with id {id} not found.", exception.Message);
    }

    [Fact]
    public async Task GetById_WhenBookExists_ReturnsExistingBook()
    {
        // Arrange
        var author = new AuthorBuilder().Tolkien
            .Build();

        var publisher = new PublisherBuilder().HarperCollins
            .Build();

        await AddAsync(author);
        await AddAsync(publisher);
        await SaveChangesAsync();

        var edition = new EditionBuilder().TheHobbit
            .WithPublisherId(publisher.Id)
            .Build();

        var book = new BookBuilder().TheHobbit
            .WithAuthorId(author.Id)
            .WithEdition(edition)
            .Build();

        await AddAsync(book);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetByIdAsync(book.Id);

        // Assert
        Assert.Equal(book.Id, actual.Id);
        Assert.Equal(book.OriginalTitle.Value, actual.OriginalTitle);
        Assert.Equal(author.Id, actual.AuthorId);
    }

    [Fact]
    public async Task GetAll_WhenNoBooksExist_ReturnsEmptyCollection()
    {
        // Arrange
        // Act
        var actual = await Sut.GetAsync();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetAll_WhenBooksExist_ReturnsAllExistingBooks()
    {
        // Arrange
        var author = new AuthorBuilder().Tolkien
            .Build();

        var publisher = new PublisherBuilder().HarperCollins
            .Build();

        await AddAsync(author);
        await AddAsync(publisher);
        await SaveChangesAsync();

        var edition1 = new EditionBuilder().TheHobbit
            .WithPublisherId(publisher.Id)
            .Build();

        var book1 = new BookBuilder().TheHobbit
            .WithAuthorId(author.Id)
            .WithEdition(edition1)
            .Build();

        var edition2 = new EditionBuilder().TheLordOfTheRings
            .WithPublisherId(publisher.Id)
            .Build();

        var book2 = new BookBuilder().TheLordOfTheRings
            .WithAuthorId(author.Id)
            .WithEdition(edition2)
            .Build();

        await AddAsync(book1);
        await AddAsync(book2);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetAsync();

        // Assert
        var books = actual.ToList();
        Assert.Equal(2, books.Count);
        Assert.Contains(books, b => b.Id == book1.Id && b.OriginalTitle == book1.OriginalTitle.Value);
        Assert.Contains(books, b => b.Id == book2.Id && b.OriginalTitle == book2.OriginalTitle.Value);
    }
}