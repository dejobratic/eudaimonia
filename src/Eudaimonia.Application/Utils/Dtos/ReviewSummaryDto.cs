namespace Eudaimonia.Application.Utils.Dtos;

public class ReviewSummaryDto
{
    public int ReviewCount { get; set; }
    public int RatingCount { get; set; }
    public int FiveStarRatingCount { get; set; }
    public int FourStarRatingCount { get; set; }
    public int ThreeStarRatingCount { get; set; }
    public int TwoStarRatingCount { get; set; }
    public int OneStarRatingCount { get; set; }
    public double AverageRating { get; set; }
}