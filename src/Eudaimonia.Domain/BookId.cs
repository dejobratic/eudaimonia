using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain;

public sealed class BookId : ValueObject<BookId>
{
    public Guid Value { get; }

    public BookId()
    {
        Value = Guid.NewGuid();
    }
}
