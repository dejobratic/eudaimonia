using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Persistence.Commands.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Builders;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Repositories;

public class PublisherRepositoryTests : CommandDbTestsBase
{
    private PublisherRepository Sut => new(DbContext);

    public PublisherRepositoryTests(CommandDbFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task AddPublisherAsync_ShouldAddPublisher()
    {
        // Arrange
        var publisher = new PublisherBuilder().HarperCollins
            .Build();

        // Act
        await Sut.AddAsync(publisher);
        await SaveChangesAsync();

        // Assert
        var actual = await FindAsync<Publisher>(p => p.Id == publisher.Id);

        Assert.Equivalent(publisher, actual);
    }
}