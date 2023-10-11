using Eudaimonia.Application.Dtos;
using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Postgres.Repositories;

namespace Eudaimonia.Infrastructure.Tests.Integration.Postgres.Repositories;

[Collection("Postgres")]
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
        var actual = await FindAsync<PublisherDto>(a => a.Id == publisher.Id.Value);

        var expected = new PublisherDto
        {
            Id = publisher.Id.Value,
            FullName = "HarperCollins",
            Bio = "HarperCollins Publishers LLC is one of the world's largest publishing companies.",
        };

        Assert.Equivalent(expected, actual);
    }
}