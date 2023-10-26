﻿using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public sealed class PublisherId : GuidId
{
    public PublisherId()
    { }

    public PublisherId(string value) : base(value)
    {
    }

    public PublisherId(Guid value) : base(value)
    {
    }
}

public sealed class Publisher : Entity<PublisherId>
{
    public Text FullName { get; }
    public Text? Bio { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Publisher() : base()
    {
    } // Required by EF Core.

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Publisher(Text fullName, Text? bio)
        : base(new PublisherId())
    {
        FullName = fullName;
        Bio = bio;

        ThrowIfInvalid();
    }

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (FullName is null) AddError(errors, nameof(FullName), $"{nameof(FullName)} must be specified.");

        return errors;
    }
}