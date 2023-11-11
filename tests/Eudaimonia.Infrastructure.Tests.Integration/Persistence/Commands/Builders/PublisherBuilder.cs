using Eudaimonia.Domain;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Builders;

public class PublisherBuilder
{
    private PublisherId _id = new();
    private Text _fullName = new("HarperCollins");
    private Text? _bio = new("HarperCollins Publishers LLC is one of the world's largest publishing companies.");

    public PublisherBuilder HarperCollins
        => WithFullName("HarperCollins")
            .WithBio("HarperCollins Publishers LLC is one of the world's largest publishing companies.");

    public PublisherBuilder PenguinRandomHouse
        => WithFullName("Penguin Random House")
            .WithBio("Penguin Random House is an American multinational conglomerate publishing company formed in 2013 from the merger of Random House and Penguin Group.");

    public PublisherBuilder WithId(PublisherId id)
    {
        _id = id;
        return this;
    }

    public PublisherBuilder WithFullName(string fullName)
    {
        _fullName = new Text(fullName);
        return this;
    }

    public PublisherBuilder WithBio(string? bio)
    {
        _bio = bio is null ? null : new Text(bio);
        return this;
    }

    public Publisher Build()
        => new(_id, _fullName, _bio);
}