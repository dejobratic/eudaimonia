﻿using Eudaimonia.Application.Dtos;
using Eudaimonia.Infrastructure.Postgres.Repositories;

namespace Eudaimonia.Infrastructure.Tests.Integration.Postgres.Repositories;

[Collection("Postgres")]
public class BookQueryRepositoryTests : RepositoryTestBase
{
    private BookQueryRepository Sut => new(DbContext);

    public BookQueryRepositoryTests(PostgresFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task GetAllBooks_()
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

        var book1 = new BookDto
        {
            Id = Guid.NewGuid(),
            Title = "The Hobbit",
            Description = "Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.",
            AuthorId = author.Id,
            Edition = new EditionDto
            {
                PageCount = 310,
                FrontCover = new ImageDto
                {
                    Name = "Cover.jpg",
                    Url = "https://pictures.abebooks.com/inventory/31499487055.jpg"
                },
                Format = "Hardcover",
                PublisherId = publisher.Id,
                PublicationYear = 1937,
            },
            ReviewSummary = new ReviewSummaryDto(),
            Genres = new[] { "Fantasy" },
        };

        var book2 = new BookDto
        {
            Id = Guid.NewGuid(),
            Title = "The Lord Of The Rings",
            Description = "Set in Middle-earth, the story began as a sequel to Tolkien's 1937 children's book The Hobbit, but eventually developed into a much larger work.",
            AuthorId = author.Id,
            Edition = new EditionDto
            {
                PageCount = 975,
                FrontCover = new ImageDto
                {
                    Name = "Cover.jpg",
                    Url = "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif"
                },
                Format = "Hardcover",
                PublisherId = publisher.Id,
                PublicationYear = 1968,
            },
            ReviewSummary = new ReviewSummaryDto(),
            Genres = new[] { "Fantasy" },
        };

        await AddAsync(author);
        await AddAsync(publisher);
        await AddAsync(book1);
        await AddAsync(book2);
        await SaveChangesAsync();

        // Act
        var actual = await Sut.GetAllAsync();

        // Assert
        var expected = new[] { book1, book2 };

        Assert.Equivalent(expected, actual);
    }
}