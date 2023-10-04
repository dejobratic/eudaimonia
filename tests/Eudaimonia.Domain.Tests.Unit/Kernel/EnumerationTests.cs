using Eudaimonia.Domain.Kernel;
using FluentAssertions;

namespace Eudaimonia.Domain.Tests.Unit.Kernel;

public class EnumerationTest
{
    private abstract class Size : Enumeration<int>
    {
        public static readonly Size Small = new SmallSize();
        public static readonly Size Medium = new MediumSize();
        public static readonly Size Large = new LargeSize();

        private Size(string name, int value)
            : base(name, value) { }

        private sealed class SmallSize : Size
        {
            public SmallSize() : base("Small", 1)
            {
            }
        }

        private sealed class MediumSize : Size
        {
            public MediumSize() : base("Medium", 2)
            {
            }
        }

        private sealed class LargeSize : Size
        {
            public LargeSize() : base("Large", 3)
            {
            }
        }
    }

    private sealed class MathConstant : Enumeration<double>
    {
        public static MathConstant Pi => new("Pi", Math.PI);
        public static MathConstant E => new("e", Math.E);

        private MathConstant(string name, double value)
            : base(name, value) { }
    }

    private sealed class StringConstant : Enumeration<string>
    {
        public static StringConstant Invalid => new(null!, "INVALID");
        public static StringConstant NullObject => new("NullObject", null!);

        private StringConstant(string name, string value) : base(name, value)
        {
        }
    }

    [Fact]
    public void Constructor_WhenNameIsNotNull_DoesNotThrowException()
    {
        Size.Small.Should().NotBeNull();
        MathConstant.Pi.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WhenNameIsNotNull_CreatesInstanceWithProvidedName()
    {
        Size.Small.Name.Should().Be("Small");
        MathConstant.Pi.Name.Should().Be("Pi");
    }

    [Fact]
    public void Constructor_WhenNameIsNull_ThrowsException()
    {
        var action = () => StringConstant.Invalid;

        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_WhenValueIsNotNull_CreatesInstanceWithProvidedValue()
    {
        Size.Small.Value.Should().Be(1);
        MathConstant.Pi.Value.Should().Be(Math.PI);
    }

    [Fact]
    public void Constructor_WhenValueIsNull_DoesNotThrowException()
    {
        StringConstant.NullObject.Should().NotBeNull();
        StringConstant.NullObject.Value.Should().BeNull();
    }

    [Fact]
    public void Equality_WhenComparingSameEnums_AreEqual()
    {
        Size.Small.Should().BeEquivalentTo(Size.Small);
        MathConstant.Pi.Should().BeEquivalentTo(MathConstant.Pi);
    }

    [Fact]
    public void ToString_Always_ReturnsName()
    {
        Size.Small.ToString().Should().Be("Small");
        MathConstant.Pi.ToString().Should().Be("Pi");
    }

    [Fact]
    public void ImplicitConversion_WhenValueIsSpecified_ConvertsToValueType()
    {
        int small = Size.Small;

        Size.Small.Value.Should().Be(small);
    }

    [Fact]
    public void GetAll_WithMemberInstances_ReturnsAll()
    {
        var items = Size.GetAll<Size>();

        items.Should().BeEquivalentTo(new[]
        {
            Size.Small,
            Size.Medium,
            Size.Large,
        });
    }

    [Fact]
    public void GetAll_WithPropertyInstances_ReturnsAll()
    {
        var items = MathConstant.GetAll<MathConstant>();

        items.Should().BeEquivalentTo(new[]
        {
            MathConstant.Pi,
            MathConstant.E,
        });
    }

    [Fact]
    public void FromValue_WhenExistingValueIsProvided_ReturnsMemberInstance()
    {
        Size instance = Size.FromValue<Size>(1);

        Size.Small.Should().Be(instance);
    }

    [Fact]
    public void FromValue_WhenExistingValueIsProvided_ReturnsPropertyInstance()
    {
        MathConstant instance = MathConstant.FromValue<MathConstant>(Math.PI);

        MathConstant.Pi.Should().Be(instance);
    }

    [Fact]
    public void FromValue_WhenNonexistentMemberValueIsProvided_ThrowsException()
    {
        var action = () => Size.FromValue<Size>(0);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void FromValue_WhenNonexistentPropertyValueIsProvided_ThrowsException()
    {
        var action = () => MathConstant.FromValue<MathConstant>(2);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void FromName_WhenNullNameIsProvided_ThrowsException()
    {
        var action = () => Size.FromName<Size>(null!);

        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void FromName_WhenExistingMemberNameIsProvided_ReturnsMemberInstance()
    {
        Size instance = Size.FromName<Size>("Small");

        instance.Should().BeEquivalentTo(Size.Small);
    }

    [Fact]
    public void FromName_WhenExistingPropertyNameIsProvided_ReturnsPropertyInstance()
    {
        MathConstant instance = MathConstant.FromName<MathConstant>("Pi");

        instance.Should().BeEquivalentTo(MathConstant.Pi);
    }

    [Fact]
    public void FromName_WhenNonexistentMemberNameIsProvided_ThrowsException()
    {
        var action = () => Size.FromName<Size>("ExtraSmall");

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void FromName_WhenNonexistentPropertyNameIsProvided_ThrowsException()
    {
        var action = () => MathConstant.FromName<MathConstant>("i");

        action.Should().Throw<ArgumentException>();
    }
}