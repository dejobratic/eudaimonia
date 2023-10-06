using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class EditionTests
{
    private static readonly uint PageCount = 310;
    private static readonly Image FrontCover = new(new Text("Cover.jpg"), "https://pictures.abebooks.com/inventory/31499487055.jpg");
    private static readonly BookFormat Format = BookFormat.Hardcover;
    private static readonly PublisherId PublisherId = new();
    private static readonly Year PublicationYear = new(1937);

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var edition = new Edition(PageCount, FrontCover, Format, PublisherId, PublicationYear);

        Assert.NotNull(edition);
        Assert.Equal(PageCount, edition.PageCount);
        Assert.Equal(FrontCover, edition.FrontCover);
        Assert.Equal(Format, edition.Format);
        Assert.Equal(PublisherId, edition.PublisherId);
        Assert.Equal(PublicationYear, edition.PublicationYear);
    }

    [Fact]
    public void Constructor_WhenPageCountIsDefault_ThrowsException()
    {
        static Edition action() => new(default, FrontCover, Format, PublisherId, PublicationYear);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Edition with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("PageCount", "PageCount must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenFrontCoverIsNull_ThrowsException()
    {
        static Edition action() => new(PageCount, null!, Format, PublisherId, PublicationYear);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Edition with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("FrontCover", "FrontCover must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenFormatIsDefault_ThrowsException()
    {
        static Edition action() => new(PageCount, FrontCover, default, PublisherId, PublicationYear);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Edition with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Format", "Format must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenPublisherIdIsNull_ThrowsException()
    {
        static Edition action() => new(PageCount, FrontCover, Format, null!, PublicationYear);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Edition with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("PublisherId", "PublisherId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenPublicationYearIsNull_ThrowsException()
    {
        static Edition action() => new(PageCount, FrontCover, Format, PublisherId, null!);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Edition with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("PublicationYear", "PublicationYear must be specified.") }, exception.Errors);
    }
}