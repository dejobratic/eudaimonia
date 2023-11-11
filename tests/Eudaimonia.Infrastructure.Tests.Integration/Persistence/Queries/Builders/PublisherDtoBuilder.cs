using Eudaimonia.Application.Dtos;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Builders;

public class PublisherDtoBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _fullName = "HarperCollins";
    private string? _bio = "HarperCollins Publishers LLC is one of the world's largest publishing companies.";

    public PublisherDtoBuilder HarperCollins
        => WithFullName("HarperCollins")
            .WithBio("HarperCollins Publishers LLC is one of the world's largest publishing companies.");

    public PublisherDtoBuilder PenguinRandomHouse
        => WithFullName("Penguin Random House")
            .WithBio("Penguin Random House is an American multinational conglomerate publishing company formed in 2013 from the merger of Random House and Penguin Group.");

    public PublisherDtoBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public PublisherDtoBuilder WithFullName(string fullName)
    {
        _fullName = fullName;
        return this;
    }

    public PublisherDtoBuilder WithBio(string? bio)
    {
        _bio = bio;
        return this;
    }

    public PublisherDto Build()
        => new() { Id = _id, FullName = _fullName, Bio = _bio };
}