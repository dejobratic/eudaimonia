using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Commands.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Builders;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Repositories;

public class BookRepositoryTests : CommandDbTestsBase
{
    private BookRepository Sut => new(DbContext);

    public BookRepositoryTests(CommandDbFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task AddBookAsync_ShouldAddBook()
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

        // Act
        await Sut.AddAsync(book);
        await SaveChangesAsync();

        // Assert
        var actual = await FindAsync<Book>(a => a.Id == book.Id);

        Assert.Equivalent(book, actual);
    }
}