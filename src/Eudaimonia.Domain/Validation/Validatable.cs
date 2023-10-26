namespace Eudaimonia.Domain.Validation;

public abstract class Validatable
{
    protected void ThrowIfInvalid()
    {
        var errors = Validate();
        if (errors.Any()) throw new ValidationException(GetType().Name, errors);
    }

    protected virtual List<ValidationError> Validate()
        => new();

    protected static void AddError(List<ValidationError> errors, string propertyName, string errorMessage)
    {
        var error = CreateError(propertyName, errorMessage);
        errors.Add(error);
    }

    private static ValidationError CreateError(string propertyName, string errorMessage)
        => new(propertyName, errorMessage);
}