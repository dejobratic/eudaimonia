using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class EditionTests
{
    private static readonly EditionId Id = new();
    private static readonly Text Title = new("The Hobbit");
    private static readonly Text Description = new("Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.");
    private static readonly Language Language = new("en");
    private static readonly EditionSpecs Specs = new(310, new Image(new Text("Cover.jpg"), "https://pictures.abebooks.com/inventory/31499487055.jpg"), BookFormat.Hardcover);
    private static readonly PublisherId PublisherId = new();
    private static readonly Year PublicationYear = new(1937);

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var edition = new Edition(Id, Title, Description, Language, Specs, PublisherId, PublicationYear);

        Assert.NotNull(edition);
        Assert.Equal(Id, edition.Id);
        Assert.Equal(Title, edition.Title);
        Assert.Equal(Description, edition.Description);
        Assert.Equal(Language, edition.Language);
        Assert.Equal(Specs, edition.Specs);
        Assert.Equal(PublisherId, edition.PublisherId);
        Assert.Equal(PublicationYear, edition.PublicationYear);
    }

    [Fact]
    public void Constructor_WhenTitleIsNull_ThrowsException()
    {
        static Edition action() => new(Id, null!, Description, Language, Specs, PublisherId, PublicationYear);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Edition with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Title", "Title must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenDescriptionIsNull_ThrowsException()
    {
        static Edition action() => new(Id, Title, null!, Language, Specs, PublisherId, PublicationYear);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Edition with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Description", "Description must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenLanguageIsNull_ThrowsException()
    {
        static Edition action() => new(Id, Title, Description, null!, Specs, PublisherId, PublicationYear);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Edition with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Language", "Language must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenSpecsIsNull_ThrowsException()
    {
        static Edition action() => new(Id, Title, Description, Language, null!, PublisherId, PublicationYear);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Edition with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Specs", "Specs must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenPublisherIdIsNull_ThrowsException()
    {
        static Edition action() => new(Id, Title, Description, Language, Specs, null!, PublicationYear);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Edition with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("PublisherId", "PublisherId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenPublicationYearIsNull_ThrowsException()
    {
        static Edition action() => new(Id, Title, Description, Language, Specs, PublisherId, null!);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Edition with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("PublicationYear", "PublicationYear must be specified.") }, exception.Errors);
    }
}