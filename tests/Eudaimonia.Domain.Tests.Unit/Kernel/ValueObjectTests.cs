using Eudaimonia.Domain.Kernel;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace Eudaimonia.Domain.Tests.Unit.Kernel;

public class ValueObjectTest
{
    [SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Members are used for equality comparison.")]
    [SuppressMessage("Critical Code Smell", "S4487:Unread \"private\" fields should be removed", Justification = "Members are used for equality comparison.")]
    private class ValueObject : ValueObject<ValueObject>
    {
        private readonly int _intField;

        private bool BoolProperty { get; }

        public string StringProperty { get; }

        public ValueObject(int intField, bool boolProperty, string stringProperty)
        {
            _intField = intField;
            BoolProperty = boolProperty;
            StringProperty = stringProperty;
        }
    }

    private class DerrivedValueObject : ValueObject
    {
        public char CharField;

        public DerrivedValueObject(int intField, bool boolProperty, string stringProperty)
            : base(intField, boolProperty, stringProperty)
        {
        }
    }

    private class ComplexValueObject : ValueObject<ComplexValueObject>
    {
        public object Object { get; }

        public ValueObject Child { get; }

        public ComplexValueObject(object obj, ValueObject child)
        {
            Object = obj;
            Child = child;
        }
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

        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Fact]
    public void GetHashCode_WhenValueObjectsAreNotEqual_AreNotEqual()
    {
        var a = new ValueObject(1, true, "a");
        var b = new ValueObject(2, true, "a");

        a.GetHashCode().Should().NotBe(b.GetHashCode());
    }

    [Fact]
    public void Derrived_ValueObjects_are_equal_if_all_their_fields_are_equal()
    {
        var a = new DerrivedValueObject(1, true, "a") { CharField = 'c' };
        var b = new DerrivedValueObject(1, true, "a") { CharField = 'c' };

        AssertAreEqual(a, b);
    }

    [Fact]
    public void Derrived_ValueObjects_are_not_equal_if_all_their_fields_are_not_equal()
    {
        var a = new DerrivedValueObject(1, true, "a") { CharField = 'c' };
        var b = new DerrivedValueObject(1, true, "a") { CharField = 'e' };

        AssertAreNotEqual(a, b);
    }

    [Fact]
    public void Derrived_ValueObjects_are_not_equal_if_all_their_base_fields_are_not_equal()
    {
        var a = new DerrivedValueObject(1, true, "a") { CharField = 'c' };
        var b = new DerrivedValueObject(2, true, "a") { CharField = 'c' };

        AssertAreNotEqual(a, b);
    }

    [Fact]
    public void Equality_WhenComparingDerivedAndBaseValueObject_AreNotEqual()
    {
        ValueObject a = new(1, true, "a");
        DerrivedValueObject b = new(2, true, "a") { CharField = 'c' };

        AssertAreNotEqual(a, b);
    }

    [Fact]
    public void Equality_WhenComparingValueObjectsOfDifferentTypes_AreNotEqual()
    {
        var a = new ValueObject(1, true, "a");

        a.Should().NotBe("");
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
        a.Should().Be(b);
        b.Should().Be(a);
        (a == b).Should().BeTrue();
        (a != b).Should().BeFalse();
    }

    private static void AssertAreNotEqual<T>(ValueObject<T>? a, ValueObject<T>? b)
        where T : ValueObject<T>
    {
        a.Should().NotBe(b);
        b.Should().NotBe(a);
        (a == b).Should().BeFalse();
        (a != b).Should().BeTrue();
    }
}