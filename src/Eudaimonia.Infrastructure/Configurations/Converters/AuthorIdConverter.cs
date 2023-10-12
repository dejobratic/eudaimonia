﻿using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Configurations.Converters;

public sealed class AuthorIdConverter : ValueConverter<AuthorId, Guid>
{
    public AuthorIdConverter()
        : base(id => id.Value, value => new AuthorId(value))
    {
    }
}