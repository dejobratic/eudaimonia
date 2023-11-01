using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Domain;
using Eudaimonia.Domain.Exceptions;
using Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Repositories;

public class BookQueryRepositoryTests : QueryDbTestsBase
{
    private BookQueryRepository Sut => new(DbContext);

    public BookQueryRepositoryTests(QueryDbFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task GetById_WhenBookDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        Task action() => Sut.GetById(id);

        // Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(action);
        Assert.Equal($"Book with id {id} not found.", exception.Message);
    }

    [Fact]
    public async Task GetById_WhenBookExists_ReturnsExistingBook()
    {
        // Arrange
        var author = new AuthorDto
        {
            Id = Guid.NewGuid(),
            FullName = "J.R.R. Tolkien",
            Bio = "John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.",
        };

        var publisher = new PublisherDto
        {
            Id = Guid.NewGuid(),
            FullName = "HarperCollins",
            Bio = "HarperCollins Publishers LLC is one of the world's largest publishing companies.",
        };

        var bookId = Guid.NewGuid();
        var editionId = Guid.NewGuid();

        var book = new BookDto
        {
            Id = bookId,
            OriginalTitle = "The Hobbit",
            OriginalLanguage = "en",
            AuthorId = author.Id,
            DefaultEditionId = editionId,
            Genres = new List<string> { "Fantasy" },
        };

        var edition = new EditionDto
        {
            Id = editionId,
            Title = "The Hobbit",
            Description = "Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.",
            Language = "en",
            Specs = new EditionSpecsDto
            {
                PageCount = 310,
                FrontCover = new ImageDto
                {
                    Name = "Cover.jpg",
                    Url = "https://pictures.abebooks.com/inventory/31499487055.jpg"
                },
                Format = "Hardcover",
            },
            BookId = bookId,
            PublisherId = publisher.Id,
            PublicationYear = 1937
        };

        await AddAsync(author);
        await AddAsync(publisher);
        await AddAsync(book);
        await AddAsync(edition);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetById(book.Id);

        // Assert
        book.Author = null!;

        Assert.Equivalent(book, actual);
    }

    [Fact]
    public async Task GetAll_WhenNoBooksExist_ReturnsEmptyCollection()
    {
        // Arrange
        // Act
        var actual = await Sut.GetAsync();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetAll_WhenBooksExist_ReturnsAllExistingBooks()
    {
        // Arrange
        var author = new AuthorDto
        {
            Id = Guid.NewGuid(),
            FullName = "J.R.R. Tolkien",
            Bio = "John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.",
        };

        var publisher = new PublisherDto
        {
            Id = Guid.NewGuid(),
            FullName = "HarperCollins",
            Bio = "HarperCollins Publishers LLC is one of the world's largest publishing companies.",
        };

        var book1Id = Guid.NewGuid();
        var edition1Id = Guid.NewGuid();

        var book2Id = Guid.NewGuid();
        var edition2Id = Guid.NewGuid();

        var book1 = new BookDto
        {
            Id = book1Id,
            OriginalTitle = "The Hobbit",
            OriginalLanguage = "en",
            AuthorId = author.Id,
            DefaultEditionId = edition1Id,
            Genres = new List<string> { "Fantasy" },
        };

        var edition1 = new List<EditionDto>
        {
            new EditionDto
            {
                Id = edition1Id,
                Title = "The Hobbit",
                Description = "Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.",
                Language = "en",
                Specs = new EditionSpecsDto
                {
                    PageCount = 310,
                    FrontCover = new ImageDto
                    {
                        Name = "Cover.jpg",
                        Url = "https://pictures.abebooks.com/inventory/31499487055.jpg"
                    },
                    Format = "Hardcover",
                },
                BookId = book1Id,
                PublisherId = publisher.Id,
                PublicationYear = 1937
            },
        };

        var book2 = new BookDto
        {
            Id = book2Id,
            OriginalTitle = "The Lord Of The Rings",
            OriginalLanguage = "en",
            AuthorId = author.Id,
            DefaultEditionId = edition2Id,
            Genres = new List<string> { "Fantasy" },
        };

        var edition2 = new EditionDto
        {
            Id = edition2Id,
            Title = "The Lord Of The Rings",
            Description = "Set in Middle-earth, the story began as a sequel to Tolkien's 1937 children's book The Hobbit, but eventually developed into a much larger work.",
            Language = "en",
            Specs = new EditionSpecsDto
            {
                PageCount = 975,
                FrontCover = new ImageDto
                {
                    Name = "Cover.jpg",
                    Url = "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif"
                },
                Format = "Hardcover",
            },
            BookId = book2Id,
            PublisherId = publisher.Id,
            PublicationYear = 1968
        };

        await AddAsync(author);
        await AddAsync(publisher);
        await AddAsync(book1);
        await AddAsync(edition1);
        await AddAsync(book2);
        await AddAsync(edition2);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetAsync();

        // Assert
        book1.Author = null!;
        book2.Author = null!;

        var expected = new[] { book1, book2 };

        Assert.Equivalent(expected, actual);
    }
}