using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class EditionSpecsTests
{
    private const int PageCount = 310;
    private static readonly Image FrontCover = new(new Text("Cover.jpg"), "https://pictures.abebooks.com/inventory/31499487055.jpg");
    private const BookFormat Format = BookFormat.Hardcover;

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var specs = new EditionSpecs(PageCount, FrontCover, Format);

        Assert.NotNull(specs);
        Assert.Equal(PageCount, specs.PageCount);
        Assert.Equal(FrontCover, specs.FrontCover);
        Assert.Equal(Format, specs.Format);
    }

    [Fact]
    public void Constructor_WhenPageCountIsDefault_ThrowsException()
    {
        static EditionSpecs Action() => new(0, FrontCover, Format);

        var exception = Assert.Throws<ValidationException>(Action);
        Assert.Equal("Validation failed for EditionSpecs with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("PageCount", "PageCount must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenFrontCoverIsNull_ThrowsException()
    {
        static EditionSpecs Action() => new(PageCount, null!, Format);

        var exception = Assert.Throws<ValidationException>(Action);
        Assert.Equal("Validation failed for EditionSpecs with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("FrontCover", "FrontCover must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenFormatIsDefault_ThrowsException()
    {
        static EditionSpecs Action() => new(PageCount, FrontCover, 0);

        var exception = Assert.Throws<ValidationException>(Action);
        Assert.Equal("Validation failed for EditionSpecs with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Format", "Format must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenFormatIsUnsupported_ThrowsException()
    {
        static EditionSpecs Action() => new(PageCount, FrontCover, (BookFormat)100);

        var exception = Assert.Throws<ValidationException>(Action);
        Assert.Equal("Validation failed for EditionSpecs with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Format", "Format must be specified.") }, exception.Errors);
    }
}