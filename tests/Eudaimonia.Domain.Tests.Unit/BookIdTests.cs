using FluentAssertions;

namespace Eudaimonia.Domain.Tests.Unit;

public class BookIdTests
{
    [Fact]
    public void Constructor_Default_CreatesInstance()
    {
        var bookId = new BookId();

        bookId.Should().NotBeNull();
        bookId.Value.Should().NotBeEmpty();
    }

    [Fact]
    public void Constructor_WhenInvokedMultipleTimes_CreatesUniqueInstances()
    {
        var bookId1 = new BookId();
        var bookId2 = new BookId();

        bookId1.Should().NotBeEquivalentTo(bookId2);
    }
}