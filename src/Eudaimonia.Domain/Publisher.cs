﻿using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public sealed class PublisherId : GuidId
{
    public PublisherId() { }

    public PublisherId(string value) : base(value) { }

    public PublisherId(Guid value) : base(value) { }
}

public sealed class Publisher : Entity<PublisherId>
{
    public Text FullName { get; }
    public Text? Bio { get; }

    private readonly HashSet<BookId> _publishedBookIds;
    public IEnumerable<BookId> PublishedBookIds => _publishedBookIds;

    public Publisher(Text fullName, Text? bio, IEnumerable<BookId> publishedBookIds)
        : base(new PublisherId())
    {
        FullName = fullName;
        Bio = bio;
        _publishedBookIds = publishedBookIds?.ToHashSet() ?? new HashSet<BookId>();

        ThrowIfInvalid();
    }

    private void ThrowIfInvalid()
    {
        if (FullName is null) ThrowValidationException(nameof(FullName), $"{nameof(FullName)} must be specified.");
        if (!PublishedBookIds.Any()) ThrowValidationException(nameof(PublishedBookIds), $"At least one {nameof(BookId)} must be specified.");
    }

    private static void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(typeof(Publisher).Name, new ValidationError(propertyName, errorMessage));
}