using Eudaimonia.Application.Dtos;
using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Extensions;

namespace Eudaimonia.Infrastructure.Tests.Unit.Extensions;

public class PublisherExtensionsTests
{
    [Fact]
    public void ToDto_GivenPublisher_ReturnsPublisherDto()
    {
        // Arrange
        var publisher = new Publisher(
            new Text("HarperCollins"),
            new Text("HarperCollins Publishers LLC is one of the world's largest publishing companies."));

        // Act
        var actual = publisher.ToDto();

        // Assert
        var expected = new PublisherDto
        {
            Id = publisher.Id.Value,
            FullName = "HarperCollins",
            Bio = "HarperCollins Publishers LLC is one of the world's largest publishing companies.",
        };

        Assert.Equivalent(expected, actual);
    }
}
