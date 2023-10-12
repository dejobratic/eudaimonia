using Eudaimonia.Application.Dtos;
using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Repositories;

namespace Eudaimonia.Infrastructure.Tests.Integration.Postgres.Repositories;

public class PublisherCommandRepositoryTests : RepositoryTestBase
{
    private PublisherCommandRepository Sut => new(DbContext);

    public PublisherCommandRepositoryTests(PostgresFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task AddPublisherAsync_ShouldAddPublisher()
    {
        // Arrange
        var publisher = new Publisher(
            new Text("HarperCollins"),
            new Text("HarperCollins Publishers LLC is one of the world's largest publishing companies."));

        // Act
        await Sut.AddAsync(publisher);

        // Assert
        var actual = await FindAsync<Publisher>(p => p.Id == publisher.Id);

        Assert.Equivalent(publisher, actual);
    }
}