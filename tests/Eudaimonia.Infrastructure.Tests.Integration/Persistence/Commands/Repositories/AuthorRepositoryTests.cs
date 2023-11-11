using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Commands.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Builders;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Repositories;

public class AuthorRepositoryTests : CommandDbTestsBase
{
    private AuthorRepository Sut => new(DbContext);

    public AuthorRepositoryTests(CommandDbFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task AddAuthorAsync_ShouldAddAuthor()
    {
        // Arrange
        var author = new AuthorBuilder().Tolkien
            .Build();

        // Act
        await Sut.AddAsync(author);
        await SaveChangesAsync();

        // Assert
        var actual = await FindAsync<Author>(a => a.Id == author.Id);

        Assert.Equivalent(author, actual);
    }
}