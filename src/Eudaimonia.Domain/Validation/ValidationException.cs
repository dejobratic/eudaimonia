using System.Runtime.Serialization;

namespace Eudaimonia.Domain.Validation;

[Serializable]
public class ValidationException : Exception
{
    public IEnumerable<ValidationError> Errors { get; } = Array.Empty<ValidationError>();

    public ValidationException(string name, IEnumerable<ValidationError> errors)
        : base($"Validation failed for {name} with {errors.Count()} error(s).")
    {
        Errors = errors;
    }

    protected ValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}