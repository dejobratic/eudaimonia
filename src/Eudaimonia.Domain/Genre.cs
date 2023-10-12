using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain;

public sealed class Genre : Enumeration<string>
{
    public static readonly Genre Action = new("Action", "Action");
    public static readonly Genre Adventure = new("Adventure", "Adventure");
    public static readonly Genre Classic = new("Classic", "Classic");
    public static readonly Genre Contemporary = new("Contemporary", "Contemporary");
    public static readonly Genre Dystopian = new("Dystopian", "Dystopian");
    public static readonly Genre Fantasy = new("Fantasy", "Fantasy");
    public static readonly Genre GraphicNovel = new("GraphicNovel", "Graphic Novel");
    public static readonly Genre Historical = new("Historical", "Historical");
    public static readonly Genre Horror = new("Horror", "Horror");
    public static readonly Genre Mystery = new("Mystery", "Mystery");
    public static readonly Genre Romance = new("Romance", "Romance");
    public static readonly Genre Satire = new("Satire", "Satire");
    public static readonly Genre ScienceFiction = new("ScienceFiction", "Science Fiction");
    public static readonly Genre ShortStory = new("ShortStory", "Short Story");
    public static readonly Genre Thriller = new("Thriller", "Thriller");
    public static readonly Genre Utopian = new("Utopian", "Utopian");
    public static readonly Genre Western = new("Western", "Western");
    public static readonly Genre ArtAndPhotography = new("ArtAndPhotography", "Art & Photography");
    public static readonly Genre Biography = new("Biography", "Biography");
    public static readonly Genre Cookbooks = new("Cookbooks", "Cookbooks");
    public static readonly Genre HowtoandDIY = new("HowtoandDIY", "How-to & DIY");
    public static readonly Genre Humor = new("Humor", "Humor");
    public static readonly Genre Parenting = new("Parenting", "Parenting");
    public static readonly Genre Autobiography = new("Autobiography", "Autobiography");
    public static readonly Genre Philosophy = new("Philosophy", "Philosophy");
    public static readonly Genre Religion = new("Religion", "Religion");
    public static readonly Genre Spirituality = new("Spirituality", "Spirituality");
    public static readonly Genre SelfHelp = new("SelfHelp", "Self-Help");
    public static readonly Genre Travel = new("Travel", "Travel");
    public static readonly Genre TrueCrime = new("TrueCrime", "True Crime");

    private Genre(string name, string value) 
        : base(name, value)
    {
    }
}