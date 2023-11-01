using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Commands.Repositories;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Repositories;

public class PublisherCommandRepositoryTests : CommandDbTestsBase
{
    private PublisherCommandRepository Sut => new(DbContext);

    public PublisherCommandRepositoryTests(CommandDbFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task AddPublisherAsync_ShouldAddPublisher()
    {
        // Arrange
        var publisher = new Publisher(
            new PublisherId(),
            new Text("HarperCollins"),
            new Text("HarperCollins Publishers LLC is one of the world's largest publishing companies."));

        // Act
        await Sut.AddAsync(publisher);
        await SaveChangesAsync();

        // Assert
        var actual = await FindAsync<Publisher>(p => p.Id == publisher.Id);

        Assert.Equivalent(publisher, actual);
    }
}