using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain.Tests.Unit.Kernel;

public class ValueObjectTest
{
    private class ValueObject(int intField, bool boolProperty, string stringProperty)
        : ValueObject<ValueObject>
    {
        private readonly int _intField = intField;

        private bool BoolProperty { get; } = boolProperty;

        public string StringProperty { get; } = stringProperty;
    }

    private class DerivedValueObject(int intField, bool boolProperty, string stringProperty)
        : ValueObject(intField, boolProperty, stringProperty)
    {
        public char CharField;
    }

    private class ComplexValueObject(object obj, ValueObject child)
        : ValueObject<ComplexValueObject>
    {
        public object Object { get; } = obj;

        public ValueObject Child { get; } = child;
    }

    [Fact]
    public void Equality_WhenValueObjectsAreNull_AreEqual()
    {
        ValueObject? a = null;
        ValueObject? b = null;

        AssertAreEqual(a, b);
    }

    [Fact]
    public void Equality_WhenValueObjectIsNull_AreNotEqual()
    {
        var a = new ValueObject(1, true, "a");

        AssertAreNotEqual(a, null);
    }

    [Fact]
    public void Equality_WhenValueObjectsHaveSamePropertiesAndKeys_AreEqual()
    {
        var a = new ValueObject(1, true, "a");
        var b = new ValueObject(1, true, "a");

        AssertAreEqual(a, b);
    }

    [Fact]
    public void Equality_WhenValueObjectsHaveAtLeastOnePrivateFieldDifferent_AreNotEqual()
    {
        var a = new ValueObject(1, true, "a");
        var b = new ValueObject(2, true, "a");

        AssertAreNotEqual(a, b);
    }

    [Fact]
    public void Equality_WhenValueObjectsHaveAtLeastOnePublicPropertyDifferent_AreNotEqual()
    {
        var a = new ValueObject(1, true, "a");
        var b = new ValueObject(1, true, "b");

        AssertAreNotEqual(a, b);
    }

    [Fact]
    public void Equality_WhenValueObjectsHaveAtLeastOnePrivatePropertyDifferent_AreNotEqual()
    {
        var a = new ValueObject(1, true, "a");
        var b = new ValueObject(1, false, "a");

        AssertAreNotEqual(a, b);
    }

    [Fact]
    public void GetHashCode_WhenValueObjectsAreEqual_AreEqual()
    {
        var a = new ValueObject(1, true, "a");
        var b = new ValueObject(1, true, "a");

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void GetHashCode_WhenValueObjectsAreNotEqual_AreNotEqual()
    {
        var a = new ValueObject(1, true, "a");
        var b = new ValueObject(2, true, "a");

        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void Derived_ValueObjects_are_equal_if_all_their_fields_are_equal()
    {
        var a = new DerivedValueObject(1, true, "a") { CharField = 'c' };
        var b = new DerivedValueObject(1, true, "a") { CharField = 'c' };

        AssertAreEqual(a, b);
    }

    [Fact]
    public void Derived_ValueObjects_are_not_equal_if_all_their_fields_are_not_equal()
    {
        var a = new DerivedValueObject(1, true, "a") { CharField = 'c' };
        var b = new DerivedValueObject(1, true, "a") { CharField = 'e' };

        AssertAreNotEqual(a, b);
    }

    [Fact]
    public void Derived_ValueObjects_are_not_equal_if_all_their_base_fields_are_not_equal()
    {
        var a = new DerivedValueObject(1, true, "a") { CharField = 'c' };
        var b = new DerivedValueObject(2, true, "a") { CharField = 'c' };

        AssertAreNotEqual(a, b);
    }

    [Fact]
    public void Equality_WhenComparingDerivedAndBaseValueObject_AreNotEqual()
    {
        ValueObject a = new(1, true, "a");
        DerivedValueObject b = new(2, true, "a") { CharField = 'c' };

        AssertAreNotEqual(a, b);
    }

    [Fact]
    public void Equality_WhenComparingValueObjectsOfDifferentTypes_AreNotEqual()
    {
        var a = new ValueObject(1, true, "a");

        Assert.NotEqual((object)"", a);
    }

    [Fact]
    public void Equality_WhenComplexValueObjectsHaveEqualFields_AreEqual()
    {
        var a = new ComplexValueObject(null!, new ValueObject(1, true, "a"));
        var b = new ComplexValueObject(null!, new ValueObject(1, true, "a"));

        AssertAreEqual(a, b);
    }

    [Fact]
    public void Equality_WhenComplexValueObjectsDontHaveEqualFields_AreNotEqual()
    {
        var a = new ComplexValueObject(null!, new ValueObject(1, true, "b"));
        var b = new ComplexValueObject(null!, new ValueObject(1, true, "a"));

        AssertAreNotEqual(a, b);
    }

    private static void AssertAreEqual<T>(ValueObject<T>? a, ValueObject<T>? b)
        where T : ValueObject<T>
    {
        Assert.Equal(a, b);
        Assert.Equal(b, a);

        Assert.True(a == b);
        Assert.True(b == a);

        Assert.False(a != b);
        Assert.False(b != a);
    }

    private static void AssertAreNotEqual<T>(ValueObject<T>? a, ValueObject<T>? b)
        where T : ValueObject<T>
    {
        Assert.NotEqual(a, b);
        Assert.NotEqual(b, a);

        Assert.False(a == b);
        Assert.False(b == a);

        Assert.True(a != b);
        Assert.True(b != a);
    }
}