namespace Eudaimonia.Application.Utils.Dtos;

public class AuthorDto : UserDto
{
    public virtual List<BookDto> Books { get; set; } = new();
}