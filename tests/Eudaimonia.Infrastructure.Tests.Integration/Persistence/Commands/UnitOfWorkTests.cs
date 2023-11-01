using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Commands;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands;

public class UnitOfWorkTests : CommandDbTestsBase
{
    private UnitOfWork Sut => new(DbContext);

    public UnitOfWorkTests(CommandDbFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task CommitAsync_ShouldCommitChanges()
    {
        // Arrange
        var author = new Author(
            new AuthorId(),
            new Text("J.R.R. Tolkien"),
            new Text("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic."));

        var publisher = new Publisher(
            new PublisherId(),
            new Text("HarperCollins"),
            new Text("HarperCollins Publishers LLC is one of the world's largest publishing companies."));

        // Act
        await AddAsync(author);
        await Sut.CommitAsync();
        await AddAsync(publisher);

        // Assert
        Assert.Equal(author, await FindAsync<Author>(a => a.Id == author.Id));
        Assert.Null(await FindAsync<Publisher>(p => p.Id == publisher.Id));
    }
}