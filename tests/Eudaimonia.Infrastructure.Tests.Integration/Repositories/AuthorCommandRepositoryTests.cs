using Eudaimonia.Application.Dtos;
using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Repositories;

namespace Eudaimonia.Infrastructure.Tests.Integration.Postgres.Repositories;

public class AuthorCommandRepositoryTests : RepositoryTestBase
{
    private AuthorCommandRepository Sut => new(DbContext);

    public AuthorCommandRepositoryTests(PostgresFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task AddAuthorAsync_ShouldAddAuthor()
    {
        // Arrange
        var author = new Author(
            new Text("J.R.R. Tolkien"),
            new Text("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic."));

        // Act
        await Sut.AddAsync(author);

        // Assert
        var actual = await FindAsync<Author>(a => a.Id == author.Id);

        Assert.Equivalent(author, actual);
    }
}