using System.Reflection;

namespace Eudaimonia.Domain.Kernel;

/// <summary>
/// Represents an abstract object-oriented enumeration. 
/// Enumeration elements should be declared as static readonly fields or properties.
/// </summary>
/// <typeparam name="TValue">Underlying enmumeration type.</typeparam>
public abstract class Enumeration<TValue>
{
    public string Name { get; }

    public TValue Value { get; }

    /// <summary> Creates a new instance (element) of the Enumeration<TValue> class.
    ///
    /// Exceptions: T:System.ArgumentNullException: element's name is null. </summary> <param
    /// name="name">Element's name.</param> <param name="value">Element's value.</param>
    protected Enumeration(string name, TValue value)
    {
        Value = value;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration<TValue> otherValue)
            return false;

        var sameType = GetType() == obj.GetType();
        var sameValue = Value?.Equals(otherValue.Value) ?? false;

        return sameType && sameValue;
    }

    public override int GetHashCode() => Value!.GetHashCode();

    public override string ToString() => Name;

    public static implicit operator TValue(Enumeration<TValue> obj)
        => obj.Value;

    /// <summary>
    /// Returns all enumeration elements.
    /// </summary>
    /// <typeparam name="T">Underlying enmumeration type.</typeparam>
    /// <returns></returns>
    public static IEnumerable<T> GetAll<T>()
        where T : Enumeration<TValue>
    {
        var fields = typeof(T).GetFields(
            BindingFlags.Public |
            BindingFlags.Static);

        var props = typeof(T).GetProperties(
            BindingFlags.Public |
            BindingFlags.Static);

        return fields
            .Select(f => f.GetValue(null))
            .OfType<T>()
            .Union(props.Select(p => p.GetValue(null)).OfType<T>());
    }

    /// <summary>
    /// Creates a new enumeration element from the given value.
    /// </summary>
    /// <typeparam name="T">Underlying enmumeration type.</typeparam>
    /// <param name="value">Value to search for.</param>
    /// <returns></returns>
    public static T FromValue<T>(TValue value)
        where T : Enumeration<TValue>
    {
        return Parse<T, TValue>(value, item => item?.Value?.Equals(value) ?? false);
    }

    /// <summary>
    /// Creates a new enumeration element from the given name.
    /// </summary>
    /// <typeparam name="T">Underlying enmumeration type.</typeparam>
    /// <param name="name">Name to search for.</param>
    /// <returns></returns>
    public static T FromName<T>(string name)
        where T : Enumeration<TValue>
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        return Parse<T, string>(name, item => item.Name == name);
    }

    private static T Parse<T, K>(K value, Func<T, bool> predicate)
        where T : Enumeration<TValue>
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate)!;
        ThrowIfNull(matchingItem, value);

        return matchingItem;
    }

    private static void ThrowIfNull<T, K>(T matchingItem, K value)
        where T : Enumeration<TValue>
    {
        if (matchingItem is null)
            throw new ArgumentException($"'{value}' is not a valid name/value in {typeof(T)} enumeration.");
    }
}