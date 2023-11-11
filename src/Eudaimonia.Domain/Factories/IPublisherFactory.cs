namespace Eudaimonia.Domain.Factories;

public interface IPublisherFactory : IEntityFactory
{
    Publisher Create(string fullName, string? bio);
}