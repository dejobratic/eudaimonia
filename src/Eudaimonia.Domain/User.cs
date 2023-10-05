﻿using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public abstract class User<T> : Entity<UserId>
    where T : User<T>
{
    public Text FullName { get; }
    public Text? Bio { get; }

    protected User(Text fullName, Text? bio)
        : base(new UserId())
    {
        FullName = fullName;
        Bio = bio;
    }

    protected virtual void ThrowIfInvalid()
    {
        if (FullName is null) ThrowValidationException(nameof(FullName), $"{nameof(FullName)} must be specified.");
    }

    protected void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(typeof(T).Name, new ValidationError(propertyName, errorMessage));
}