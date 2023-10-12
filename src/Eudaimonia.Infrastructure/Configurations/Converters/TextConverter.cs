﻿using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Configurations.Converters;

public sealed class TextConverter : ValueConverter<Text, string>
{
    public TextConverter()
        : base(text => text.Value, value => new Text(value))
    {
    }
}