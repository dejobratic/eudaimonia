using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Commands.Repositories;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Repositories;

public class AuthorCommandRepositoryTests : CommandDbTestsBase
{
    private AuthorCommandRepository Sut => new(DbContext);

    public AuthorCommandRepositoryTests(CommandDbFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task AddAuthorAsync_ShouldAddAuthor()
    {
        // Arrange
        var author = new Author(
            new AuthorId(),
            new Text("J.R.R. Tolkien"),
            new Text("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic."));

        // Act
        await Sut.AddAsync(author);
        await SaveChangesAsync();

        // Assert
        var actual = await FindAsync<Author>(a => a.Id == author.Id);

        Assert.Equivalent(author, actual);
    }
}