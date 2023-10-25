﻿using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public abstract class GuidId : ValueObject<GuidId>
{
    public Guid Value { get; private set; }

    protected GuidId() : this(Guid.NewGuid())
    {
    }

    protected GuidId(Guid value)
    {
        Value = value;
        ThrowIfInvalid();
    }

    protected GuidId(string value)
    {
        _ = Guid.TryParse(value, out var guid);
        Value = guid;
        ThrowIfInvalid();
    }

    public void ThrowIfInvalid()
    {
        if (Value == Guid.Empty)
            throw new ValidationException(GetType().Name, new ValidationError(nameof(Value), "Value must be a valid non-empty Guid or Guid string."));
    }

    public override string ToString() 
        => Value.ToString();

    public static implicit operator Guid(GuidId id) 
        => id is null ? Guid.Empty : id.Value;
}