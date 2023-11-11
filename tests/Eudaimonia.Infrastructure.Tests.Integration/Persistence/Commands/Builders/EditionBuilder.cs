using Eudaimonia.Domain;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Builders;

public class EditionBuilder
{
    private EditionId _id = new();
    private Text _title = new("The Hobbit");
    private Text _description = new("Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.");
    private Language _language = new("en");
    private int _pageCount = 310;
    private Image _frontCover = new(new Text("Cover.jpg"), "https://pictures.abebooks.com/inventory/31499487055.jpg");
    private BookFormat _format = BookFormat.Hardcover;
    private PublisherId _publisherId = new();
    private Year _publicationYear = new(1937);

    public EditionBuilder TheHobbit
        => WithTitle("The Hobbit")
            .WithDescription("Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.")
            .WithLanguage("en")
            .WithPageCount(310)
            .WithFrontCover("Cover.jpg", "https://pictures.abebooks.com/inventory/31499487055.jpg")
            .WithFormat(BookFormat.Hardcover)
            .WithPublicationYear(1937);

    public EditionBuilder TheLordOfTheRings
        => WithTitle("The Lord of the Rings")
            .WithDescription("The Lord of the Rings is an epic high-fantasy novel written by English author and scholar J. R. R. Tolkien.")
            .WithLanguage("en")
            .WithPageCount(975)
            .WithFrontCover("Cover.jpg", "https://pictures.abebooks.com/inventory/31499487055.jpg")
            .WithFormat(BookFormat.Hardcover)
            .WithPublicationYear(1954);

    public EditionBuilder WithId(EditionId id)
    {
        _id = id;
        return this;
    }

    public EditionBuilder WithTitle(string title)
    {
        _title = new Text(title);
        return this;
    }

    public EditionBuilder WithDescription(string description)
    {
        _description = new Text(description);
        return this;
    }

    public EditionBuilder WithLanguage(string language)
    {
        _language = new Language(language);
        return this;
    }

    public EditionBuilder WithPageCount(int pageCount)
    {
        _pageCount = pageCount;
        return this;
    }

    public EditionBuilder WithFrontCover(string frontCoverName, string frontCoverUrl)
    {
        _frontCover = new Image(new Text(frontCoverName), frontCoverUrl);
        return this;
    }

    public EditionBuilder WithFormat(BookFormat format)
    {
        _format = format;
        return this;
    }

    public EditionBuilder WithPublisherId(PublisherId publisherId)
    {
        _publisherId = publisherId;
        return this;
    }

    public EditionBuilder WithPublicationYear(int publicationYear)
    {
        _publicationYear = new Year(publicationYear);
        return this;
    }

    public Edition Build()
        => new(
            _id,
            _title,
            _description,
            _language,
            new EditionSpecs(_pageCount, _frontCover, _format),
            _publisherId,
            _publicationYear);
}