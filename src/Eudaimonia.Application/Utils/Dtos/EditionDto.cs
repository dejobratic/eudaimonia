namespace Eudaimonia.Application.Utils.Dtos;

public class EditionDto
{
    public int PageCount { get; set; }
    public ImageDto FrontCover { get; set; } = null!;
    public string Format { get; set; } = null!;
    public Guid PublisherId { get; set; }
    public int PublicationYear { get; set; }
}