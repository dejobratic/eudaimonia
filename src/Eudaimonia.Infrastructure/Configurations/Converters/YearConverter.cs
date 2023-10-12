﻿using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Configurations.Converters;

public sealed class YearConverter : ValueConverter<Year, int>
{
    public YearConverter()
        : base(year => year.Value, value => new Year(value))
    {
    }
}