namespace Eudaimonia.Domain.Kernel;

/// <summary>
/// Represents an abstract entity object in domain-driven design. Entities are distinguished by
/// their id property.
/// </summary>
/// <typeparam name="TId">Entity ID's type.</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    public TId Id { get; protected set; }

    protected Entity() { } // Required by EF Core.

    protected Entity(TId id)
    {
        ThrowIfDefault(id);
        Id = id;
    }

    public override bool Equals(object? obj)
        => Equals(obj as Entity<TId>);

    public bool Equals(Entity<TId>? other)
        => other is not null && Equals(Id, other.Id);

    public override int GetHashCode()
        => Id!.GetHashCode();

    public static bool operator ==(Entity<TId>? x, Entity<TId>? y)
        => Equals(x, y);

    public static bool operator !=(Entity<TId>? x, Entity<TId>? y)
        => !(x == y);

    private static void ThrowIfDefault(TId id)
    {
        if (Equals(id, default(TId)))
            throw new ArgumentException("A default value cannot be used as Entity Id.", nameof(id));
    }
}