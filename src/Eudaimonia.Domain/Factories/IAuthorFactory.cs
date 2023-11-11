namespace Eudaimonia.Domain.Factories;

public interface IAuthorFactory : IEntityFactory
{
    Author Create(string fullName, string? bio);
}