namespace Eudaimonia.Domain.Validation;

public class ValidationError
{
    public string Member { get; }
    public string Message { get; }

    public ValidationError(string member, string message)
    {
        Member = member;
        Message = message;
    }
}