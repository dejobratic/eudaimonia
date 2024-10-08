﻿using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Configurations.Converters;

public sealed class PublisherIdConverter : ValueConverter<PublisherId, Guid>
{
    public PublisherIdConverter()
        : base(id => id.Value, value => new PublisherId(value))
    {
    }
}