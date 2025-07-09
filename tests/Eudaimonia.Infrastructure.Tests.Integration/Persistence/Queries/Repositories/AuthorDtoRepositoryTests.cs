using Eudaimonia.Infrastructure.Persistence.Queries.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Builders;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Repositories;

public class AuthorDtoRepositoryTests(QueryDbFixture fixture) : QueryDbTestsBase(fixture)
{
    private AuthorDtoRepository Sut => new(DbContext);

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
        var author1 = new AuthorBuilder().Tolkien
            .Build();

        var author2 = new AuthorBuilder().Rowling
            .Build();

        await AddAsync(author1);
        await AddAsync(author2);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetAsync();

        // Assert
        var authors = actual.ToList();
        Assert.Equal(2, authors.Count);
        Assert.Contains(authors, a => a.Id == author1.Id && a.FullName == author1.FullName.Value);
        Assert.Contains(authors, a => a.Id == author2.Id && a.FullName == author2.FullName.Value);
    }
}