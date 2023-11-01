using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Configurations.Converters;

public sealed class LanguageConverter : ValueConverter<Language, string>
{
    public LanguageConverter()
        : base(language => language.Code, value => new Language(value))
    {
    }
}