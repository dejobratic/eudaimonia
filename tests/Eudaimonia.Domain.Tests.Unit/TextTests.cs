using Eudaimonia.Domain.Validation;
using FluentAssertions;

namespace Eudaimonia.Domain.Tests.Unit;

public class TextTests
{
    [Fact]
    public void Constructor_WhenProvidedValidValue_CreatesInstance()
    {
        var value = "The Hobbit";

        var text = new Text(value);

        text.Should().NotBeNull();
        text.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_WhenProvidedInvalidValue_ThrowsException(string value)
    {
        var action = () => new Text(value);

        action.Should().Throw<ValidationException>()
            .WithMessage("Validation failed for Text with 1 error(s).")
            .And.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationError("Value", "Value cannot be null or empty."),
            });
    }

    [Fact]
    public void ToString_Always_ReturnsValue()
    {
        var value = "The Hobbit";
        var text = new Text(value);

        text.ToString().Should().Be(value);
    }
}