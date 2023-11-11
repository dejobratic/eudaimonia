using Eudaimonia.Domain;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Builders;

public class AuthorBuilder
{
    private AuthorId _id = new();
    private Text _fullName = new("J. R. R. Tolkien");
    private Text? _bio = new("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.");

    public AuthorBuilder Tolkien
        => WithFullName("J. R. R. Tolkien")
            .WithBio("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.");

    public AuthorBuilder Rowling
        => WithFullName("J.K. Rowling")
            .WithBio("Joanne Rowling CH, OBE, HonFRSE, FRCPE, FRSL, better known by her pen name J. K. Rowling, is a British author, philanthropist, film producer, television producer, and screenwriter.");

    public AuthorBuilder WithId(AuthorId id)
    {
        _id = id;
        return this;
    }

    public AuthorBuilder WithFullName(string fullName)
    {
        _fullName = new Text(fullName);
        return this;
    }

    public AuthorBuilder WithBio(string? bio)
    {
        _bio = bio is null ? null : new Text(bio);
        return this;
    }

    public Author Build()
        => new(_id, _fullName, _bio);
}