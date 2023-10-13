using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Postgres;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Repositories;

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