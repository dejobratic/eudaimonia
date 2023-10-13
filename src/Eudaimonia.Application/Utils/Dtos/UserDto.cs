namespace Eudaimonia.Application.Utils.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? Bio { get; set; }
}