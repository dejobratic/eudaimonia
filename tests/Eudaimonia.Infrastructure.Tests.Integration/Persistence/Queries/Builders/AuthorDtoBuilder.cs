using Eudaimonia.Application.Dtos;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Builders;

public class AuthorDtoBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _fullName = "J. R.R. Tolkien";
    private string? _bio = "John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.";

    public AuthorDtoBuilder Tolkien
        => WithFullName("J. R. R. Tolkien")
            .WithBio("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.");

    public AuthorDtoBuilder Rowling
        => WithFullName("J.K. Rowling")
            .WithBio("Joanne Rowling CH, OBE, HonFRSE, FRCPE, FRSL, better known by her pen name J. K. Rowling, is a British author, philanthropist, film producer, television producer, and screenwriter.");

    public AuthorDtoBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public AuthorDtoBuilder WithFullName(string fullName)
    {
        _fullName = fullName;
        return this;
    }

    public AuthorDtoBuilder WithBio(string? bio)
    {
        _bio = bio;
        return this;
    }

    public AuthorDto Build()
        => new() { Id = _id, FullName = _fullName, Bio = _bio };
}