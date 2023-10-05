using System.Collections;

namespace Eudaimonia.Domain.Kernel;

/// <summary>
/// Represents an abstract value object in domain-driven design. Value objects are distinguished by
/// the data that they hold.
/// </summary>
/// <typeparam name="T">The underlying value object's type.</typeparam>
public abstract class ValueObject<T> : IEquatable<T>
    where T : ValueObject<T>
{
    private static readonly object _lock = new();

    private static readonly IDictionary<Type, IEqualityComparer> _comparers =
        new Dictionary<Type, IEqualityComparer> { [typeof(T)] = CreateEqualityComparer(typeof(T)) };

    public override bool Equals(object? obj)
        => Equals(obj as T);

    public virtual bool Equals(T? other)
    {
        if (other is null)
            return false;

        if (!HasSameRuntimeTypeAs(other))
            return false;

        return GetEqualityComparer().Equals(this, other);
    }

    public override int GetHashCode()
    {
        return GetEqualityComparer().GetHashCode(this);
    }

    public static bool operator ==(ValueObject<T>? x, ValueObject<T>? y)
        => Equals(x, y);

    public static bool operator !=(ValueObject<T>? x, ValueObject<T>? y)
        => !Equals(x, y);

    private IEqualityComparer GetEqualityComparer()
    {
        Type runtimeType = GetType();

        if (_comparers.TryGetValue(runtimeType, out IEqualityComparer? fec))
            return fec;

        lock (_lock)
        {
            if (!_comparers.ContainsKey(runtimeType))
                _comparers[runtimeType] = CreateEqualityComparer(runtimeType);
        }

        return _comparers[runtimeType];
    }

    private static FieldEqualityComparer CreateEqualityComparer(Type type)
        => new(type);

    private bool HasSameRuntimeTypeAs(T other)
        => GetType() == other.GetType();
}