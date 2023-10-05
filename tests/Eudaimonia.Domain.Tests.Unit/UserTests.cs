using Eudaimonia.Domain.Validation;
using FluentAssertions;

namespace Eudaimonia.Domain.Tests.Unit;

public class UserTests
{
    private static readonly Text UserFullName = new("John Doe");
    private static readonly Text UserBio = new("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

    [Fact]
    public void Constructor_WhenProvidingAllRequiredParameters_CreatesInstance()
    {
        var user = new User(UserFullName, UserBio);

        user.Id.Should().NotBeNull();
        user.FullName.Should().Be(UserFullName);
        user.Bio.Should().Be(UserBio);
    }

    [Fact]
    public void Constructor_WhenFullNameIsNull_ThrowsException()
    {
        var action = () => new User(null!, UserBio);

        action.Should().Throw<ValidationException>()
            .WithMessage("Validation failed for User with 1 error(s).")
            .And.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationError("FullName", "FullName must be specified."),
            });
    }

    [Fact]
    public void Constructor_WhenBioIsNull_CreatesInstance()
    {
        var user = new User(UserFullName, null);

        user.Id.Should().NotBeNull();
        user.FullName.Should().Be(UserFullName);
        user.Bio.Should().BeNull();
    }
}
