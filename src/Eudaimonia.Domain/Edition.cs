using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;


public sealed class EditionId : GuidId
{
    public EditionId()
    { }

    public EditionId(string value) : base(value)
    {
    }

    public EditionId(Guid value) : base(value)
    {
    }
}
public sealed class Edition : Entity<EditionId>
{
    public Text Title { get; } = null!;
    public Text Description { get; } = null!;
    public Language Language { get; } = null!;
    public EditionSpecs Specs { get; } = null!;
    public PublisherId PublisherId { get; } = null!;
    public Year PublicationYear { get; } = null!;

    private Edition() : base()
    {
    } // Required by EF Core.

    public Edition(
        EditionId id,
        Text title,
        Text description,
        Language language,
        EditionSpecs specs,
        PublisherId publisherId,
        Year publicationYear)
        : base(id)
    {
        Title = title;
        Description = description;
        Language = language;
        Specs = specs;
        PublisherId = publisherId;
        PublicationYear = publicationYear;

        ThrowIfInvalid();
    }

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (Title is null) AddError(errors, nameof(Title), $"{nameof(Title)} must be specified.");
        if (Description is null) AddError(errors, nameof(Description), $"{nameof(Description)} must be specified.");
        if (Language is null) AddError(errors, nameof(Language), $"{nameof(Language)} must be specified.");
        if (Specs is null) AddError(errors, nameof(Specs), $"{nameof(Specs)} must be specified.");
        if (PublisherId is null) AddError(errors, nameof(PublisherId), $"{nameof(PublisherId)} must be specified.");
        if (PublicationYear is null) AddError(errors, nameof(PublicationYear), $"{nameof(PublicationYear)} must be specified.");

        return errors;
    }
}