using Eudaimonia.Infrastructure.Persistence.Queries.Repositories;
using Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Builders;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Repositories;

public class PublisherDtoRepositoryTests : QueryDbTestsBase
{
    private PublisherDtoRepository Sut => new(DbContext);

    public PublisherDtoRepositoryTests(QueryDbFixture fixture)
        : base(fixture)
    {
    }

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
        var publisher1 = new PublisherDtoBuilder().HarperCollins
            .Build();
        
        var publisher2 = new PublisherDtoBuilder().PenguinRandomHouse
            .Build();

        await AddAsync(publisher1);
        await AddAsync(publisher2);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetAsync();

        // Assert
        var expected = new[] { publisher1, publisher2 };

        Assert.Equivalent(expected, actual);
    }
}