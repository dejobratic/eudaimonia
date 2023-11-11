using Eudaimonia.Application.Dtos;
using Eudaimonia.Domain.Exceptions;
using Eudaimonia.Infrastructure.Persistence.Queries.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Builders;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Repositories;

public class BookDtoRepositoryTests : QueryDbTestsBase
{
    private BookDtoRepository Sut => new(DbContext);

    public BookDtoRepositoryTests(QueryDbFixture fixture)
        : base(fixture)
    {
    }

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
        var author = new AuthorDtoBuilder().Tolkien
            .Build();

        var publisher = new PublisherDtoBuilder().HarperCollins
            .Build();

        var book = new BookDtoBuilder().TheHobbit
            .WithAuthorId(author.Id)
            .Build();

        var edition = new EditionDtoBuilder().TheHobbit
            .WithId(book.DefaultEditionId)
            .WithBookId(book.Id)
            .WithPublisherId(publisher.Id)
            .Build();

        await AddAsync(author);
        await AddAsync(publisher);
        await AddAsync(book);
        await AddAsync(edition);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetByIdAsync(book.Id);

        // Assert
        book.Author = null!;

        Assert.Equivalent(book, actual);
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
        var author = new AuthorDtoBuilder().Tolkien
            .Build();

        var publisher = new PublisherDtoBuilder().HarperCollins
            .Build();

        var book1 = new BookDtoBuilder().TheHobbit
            .WithAuthorId(author.Id)
            .Build();

        var edition1 = new EditionDtoBuilder().TheHobbit
            .WithId(book1.DefaultEditionId)
            .WithBookId(book1.Id)
            .WithPublisherId(publisher.Id)
            .Build();

        var book2 = new BookDtoBuilder().TheLordOfTheRings
            .WithAuthorId(author.Id)
            .Build();

        var edition2 = new EditionDtoBuilder().TheLordOfTheRings
            .WithId(book2.DefaultEditionId)
            .WithBookId(book2.Id)
            .WithPublisherId(publisher.Id)
            .Build();

        await AddAsync(author);
        await AddAsync(publisher);
        await AddAsync(book1);
        await AddAsync(edition1);
        await AddAsync(book2);
        await AddAsync(edition2);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetAsync();

        // Assert
        book1.Author = null!;
        book2.Author = null!;

        var expected = new[] { book1, book2 };

        Assert.Equivalent(expected, actual);
    }
}