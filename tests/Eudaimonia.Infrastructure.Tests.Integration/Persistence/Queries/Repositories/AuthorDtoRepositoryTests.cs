using Eudaimonia.Infrastructure.Persistence.Queries.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Builders;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Repositories;

public class AuthorDtoRepositoryTests : QueryDbTestsBase
{
    private AuthorDtoRepository Sut => new(DbContext);

    public AuthorDtoRepositoryTests(QueryDbFixture fixture)
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
        var author1 = new AuthorDtoBuilder().Tolkien
            .Build();

        var author2 = new AuthorDtoBuilder().Rowling
            .Build();

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