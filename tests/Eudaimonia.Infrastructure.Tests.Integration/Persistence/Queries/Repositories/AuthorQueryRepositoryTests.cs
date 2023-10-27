using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Repositories;

public class AuthorQueryRepositoryTests : QueryDbTestsBase
{
    private AuthorQueryRepository Sut => new(DbContext);

    public AuthorQueryRepositoryTests(QueryDbFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task GetAll_WhenNoAuthorsExist_ReturnsEmptyCollection()
    {
        // Arrange
        // Act
        var actual = await Sut.GetAsync();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetAll_WhenAuthorsExist_ReturnsAllExistingAuthors()
    {
        // Arrange
        var author1 = new AuthorDto
        {
            Id = Guid.NewGuid(),
            FullName = "J.R.R. Tolkien",
            Bio = "John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic."
        };

        var author2 = new AuthorDto
        {
            Id = Guid.NewGuid(),
            FullName = "J.K. Rowling",
            Bio = "Joanne Rowling CH, OBE, HonFRSE, FRCPE, FRSL, better known by her pen name J. K. Rowling, is a British author, philanthropist, film producer, television producer, and screenwriter."
        };

        await AddAsync(author1);
        await AddAsync(author2);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetAsync();

        // Assert
        var expected = new[] { author1, author2 };

        Assert.Equivalent(expected, actual);
    }
}