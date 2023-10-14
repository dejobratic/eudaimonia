using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

//TODO: This should be a way to link multiple editions of a same book.
public sealed class Edition : ValueObject<Edition>
{
    public uint PageCount { get; }
    public Image FrontCover { get; }
    public BookFormat Format { get; }
    public PublisherId PublisherId { get; }
    public Year PublicationYear { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Edition() : base() { } // Required by EF Core.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Edition(
        uint pageCount,
        Image frontCover,
        BookFormat format,
        PublisherId publisherId,
        Year publicationYear)
    {
        PageCount = pageCount;
        FrontCover = frontCover;
        Format = format;
        PublisherId = publisherId;
        PublicationYear = publicationYear;

        ThrowIfInvalid();
    }

    private void ThrowIfInvalid()
    {
        if (PageCount == default) ThrowValidationException(nameof(PageCount), $"{nameof(PageCount)} must be specified.");
        if (FrontCover is null) ThrowValidationException(nameof(FrontCover), $"{nameof(FrontCover)} must be specified.");
        if (Format == default) ThrowValidationException(nameof(Format), $"{nameof(Format)} must be specified.");
        if (PublisherId is null) ThrowValidationException(nameof(PublisherId), $"{nameof(PublisherId)} must be specified.");
        if (PublicationYear is null) ThrowValidationException(nameof(PublicationYear), $"{nameof(PublicationYear)} must be specified.");
    }

    private void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(nameof(Edition), new ValidationError(propertyName, errorMessage));
}