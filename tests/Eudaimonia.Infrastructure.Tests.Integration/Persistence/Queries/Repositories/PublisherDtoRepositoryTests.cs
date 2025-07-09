using Eudaimonia.Infrastructure.Persistence.Queries.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Builders;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Repositories;

public class PublisherDtoRepositoryTests(QueryDbFixture fixture) : QueryDbTestsBase(fixture)
{
    private PublisherDtoRepository Sut => new(DbContext);

    [Fact]
    public async Task GetAll_WhenNoPublishersExist_ReturnsEmptyCollection()
    {
        // Arrange
        // Act
        var actual = await Sut.GetAsync();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetAll_WhenPublishersExist_ReturnsAllExistingPublishers()
    {
        // Arrange
        var publisher1 = new PublisherBuilder().HarperCollins
            .Build();
        
        var publisher2 = new PublisherBuilder().PenguinRandomHouse
            .Build();

        await AddAsync(publisher1);
        await AddAsync(publisher2);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetAsync();

        // Assert
        var publishers = actual.ToList();
        Assert.Equal(2, publishers.Count);
        Assert.Contains(publishers, p => p.Id == publisher1.Id && p.FullName == publisher1.FullName.Value);
        Assert.Contains(publishers, p => p.Id == publisher2.Id && p.FullName == publisher2.FullName.Value);
    }
}