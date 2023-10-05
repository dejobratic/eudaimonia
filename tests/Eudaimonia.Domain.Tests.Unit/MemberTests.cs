using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class MemberTests
{
    private static readonly Text MemberFullName = new("John Doe");
    private static readonly Text MemberBio = new("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var user = new Member(MemberFullName, MemberBio);

        Assert.NotNull(user.Id);
        Assert.Equal(MemberFullName, user.FullName);
        Assert.Equal(MemberBio, user.Bio);
    }

    [Fact]
    public void Constructor_WhenFullNameIsNull_ThrowsException()
    {
        static Member action() => new(null!, MemberBio);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Member with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("FullName", "FullName must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenBioIsNull_CreatesInstance()
    {
        var user = new Member(MemberFullName, null);

        Assert.NotNull(user.Id);
        Assert.Equal(MemberFullName, user.FullName);
        Assert.Null(user.Bio);
    }
}