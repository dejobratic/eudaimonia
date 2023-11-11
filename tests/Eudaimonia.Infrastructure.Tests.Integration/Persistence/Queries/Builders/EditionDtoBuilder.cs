using Eudaimonia.Application.Dtos;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Builders;

public class EditionDtoBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _title = "The Hobbit";
    private string _description = "Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.";
    private string _language = "en";

    private readonly EditionSpecsDto _specs = new()
    {
        PageCount = 310,
        FrontCover = new ImageDto
        {
            Name = "Cover.jpg",
            Url = "https://pictures.abebooks.com/inventory/31499487055.jpg"
        },
        Format = "Hardcover",
    };

    private Guid _bookId = Guid.NewGuid();
    private Guid _publisherId = Guid.NewGuid();
    private int _publicationYear = 1937;

    public EditionDtoBuilder TheHobbit
        => WithTitle("The Hobbit")
            .WithDescription("Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.")
            .WithLanguage("en")
            .WithPageCount(310)
            .WithFrontCover("Cover.jpg", "https://pictures.abebooks.com/inventory/31499487055.jpg")
            .WithFormat("Hardcover")
            .WithPublicationYear(1937);

    public EditionDtoBuilder TheLordOfTheRings
        => WithTitle("The Lord of the Rings")
            .WithDescription("The Lord of the Rings is an epic high-fantasy novel written by English author and scholar J. R. R. Tolkien.")
            .WithLanguage("en")
            .WithPageCount(975)
            .WithFrontCover("Cover.jpg", "https://pictures.abebooks.com/inventory/31499487055.jpg")
            .WithFormat("Hardcover")
            .WithPublicationYear(1954);

    public EditionDtoBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public EditionDtoBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public EditionDtoBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public EditionDtoBuilder WithLanguage(string language)
    {
        _language = language;
        return this;
    }

    public EditionDtoBuilder WithPageCount(int pageCount)
    {
        _specs.PageCount = pageCount;
        return this;
    }

    public EditionDtoBuilder WithFrontCover(string name, string url)
    {
        _specs.FrontCover = new ImageDto { Name = name, Url = url };
        return this;
    }

    public EditionDtoBuilder WithFormat(string format)
    {
        _specs.Format = format;
        return this;
    }

    public EditionDtoBuilder WithBookId(Guid bookId)
    {
        _bookId = bookId;
        return this;
    }

    public EditionDtoBuilder WithPublisherId(Guid publisherId)
    {
        _publisherId = publisherId;
        return this;
    }

    public EditionDtoBuilder WithPublicationYear(int publicationYear)
    {
        _publicationYear = publicationYear;
        return this;
    }

    public EditionDto Build()
        => new()
        {
            Id = _id,
            Title = _title,
            Description = _description,
            Language = _language,
            Specs = _specs,
            BookId = _bookId,
            PublisherId = _publisherId,
            PublicationYear = _publicationYear
        };
}