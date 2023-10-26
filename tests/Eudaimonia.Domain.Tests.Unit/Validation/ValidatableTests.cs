using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit.Validation;

public class ValidatableTests
{
    private class ValidatableEntity : Validatable
    {
        public int Id { get; }
        public string Name { get; }

        public ValidatableEntity(int id, string name)
        {
            Id = id;
            Name = name;

            ThrowIfInvalid();
        }

        protected override List<ValidationError> Validate()
        {
            var errors = base.Validate();

            if (Id <= 0) AddError(errors, nameof(Id), $"{nameof(Id)} must be greater than 0.");
            if (string.IsNullOrEmpty(Name)) AddError(errors, nameof(Name), $"{nameof(Name)} must be specified.");

            return errors;
        }
    }

    [Fact]
    public void Constructor_WhenValidatableEntityIsValid_DoesNotThrow()
    {
        var exception = Record.Exception(() => new ValidatableEntity(1, "John Doe"));

        Assert.Null(exception);
    }

    [Fact]
    public void Constructor_WhenValidatableEntityIsInvalid_Throws()
    {
        static ValidatableEntity action() => new(-1, null!);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for ValidatableEntity with 2 error(s).", exception.Message);
        Assert.Equivalent(new[]
        {
            new ValidationError("Id", "Id must be greater than 0."),
            new ValidationError("Name", "Name must be specified.")
        }, exception.Errors);
    }
}