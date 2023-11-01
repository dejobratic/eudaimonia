using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class MemberTests
{
    private static readonly UserId Id = new();
    private static readonly Text FullName = new("John Doe");
    private static readonly Text Bio = new("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var user = new Member(Id, FullName, Bio);

        Assert.NotNull(user);
        Assert.Equal(Id, user.Id);
        Assert.Equal(FullName, user.FullName);
        Assert.Equal(Bio, user.Bio);
    }

    [Fact]
    public void Constructor_WhenFullNameIsNull_ThrowsException()
    {
        static Member action() => new(Id, null!, Bio);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Member with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("FullName", "FullName must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenBioIsNull_CreatesInstance()
    {
        var user = new Member(Id, FullName, null);

        Assert.Equal(Id, user.Id);
        Assert.Equal(FullName, user.FullName);
        Assert.Null(user.Bio);
    }
}