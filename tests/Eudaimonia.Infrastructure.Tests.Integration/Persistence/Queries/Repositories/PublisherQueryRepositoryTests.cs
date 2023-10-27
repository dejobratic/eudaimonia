using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Repositories;

public class PublisherQueryRepositoryTests : QueryDbTestsBase
{
    private PublisherQueryRepository Sut => new(DbContext);

    public PublisherQueryRepositoryTests(QueryDbFixture fixture)
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
        var publisher1 = new PublisherDto
        {
            Id = Guid.NewGuid(),
            FullName = "HarperCollins",
            Bio = "HarperCollins Publishers LLC is one of the world's largest publishing companies."
        };

        var publisher2 = new PublisherDto
        {
            Id = Guid.NewGuid(),
            FullName = "Penguin Random House",
            Bio = "Penguin Random House is an American multinational conglomerate publishing company formed in 2013 from the merger of Random House and Penguin Group."
        };

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