using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public class EditionSpecs : ValueObject<EditionSpecs>
{
    public int PageCount { get; }
    public Image FrontCover { get; } = null!;
    public BookFormat Format { get; }

    private EditionSpecs()
    {
    } // Required by EF Core.

    public EditionSpecs(int pageCount, Image frontCover, BookFormat format)
    {
        PageCount = pageCount;
        FrontCover = frontCover;
        Format = format;

        ThrowIfInvalid();
    }

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (PageCount == default) AddError(errors, nameof(PageCount), $"{nameof(PageCount)} must be specified.");
        if (FrontCover is null) AddError(errors, nameof(FrontCover), $"{nameof(FrontCover)} must be specified.");
        if (Format == default || !Enum.IsDefined(Format)) AddError(errors, nameof(Format), $"{nameof(Format)} must be specified.");

        return errors;
    }
}