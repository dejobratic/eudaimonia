namespace Eudaimonia.Application.Dtos;

public class EditionSpecsDto
{
    public int? PageCount { get; set; }
    public ImageDto? FrontCover { get; set; } = null!;
    public string? Format { get; set; } = null!;
}