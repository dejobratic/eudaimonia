using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class TextTests
{
    [Fact]
    public void Constructor_WhenProvidedValidValue_CreatesInstance()
    {
        var value = "The Hobbit";

        var text = new Text(value);

        Assert.NotNull(text);
        Assert.Equal(value, text.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_WhenProvidedInvalidValue_ThrowsException(string value)
    {
        Text action() => new(value);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Text with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Value", "Value cannot be null or empty.") }, exception.Errors);
    }

    [Fact]
    public void ToString_Always_ReturnsValue()
    {
        var value = "The Hobbit";
        var text = new Text(value);

        Assert.Equal(value, text.ToString());
    }
}