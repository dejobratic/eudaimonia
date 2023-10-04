using FluentAssertions;

namespace Eudaimonia.Domain.Tests.Unit;

public class BookTests
{
    [Fact]
    public void Constructor_WhenProvidingAllRequiredProperties_CreatesInstance()
    {
        var title = new Text("The Hobbit");
        var description = new Text("The Hobbit is a fantasy novel by English author J. R. R. Tolkien.");
        var genres = new[] { Genre.Fantasy };

        var book = new Book(title, description, genres);

        book.Id.Should().NotBeNull();
        book.Title.Should().Be(title);
        book.Description.Should().Be(description);
        book.Genres.Should().BeEquivalentTo(genres);
    }
}
