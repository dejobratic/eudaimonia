namespace Eudaimonia.Application.Utils.Dtos;

public class AuthorDto : UserDto
{
    private readonly List<Guid> _authoredBookIds = new();
    public IReadOnlyList<Guid> AuthoredBookIds => _authoredBookIds;
}