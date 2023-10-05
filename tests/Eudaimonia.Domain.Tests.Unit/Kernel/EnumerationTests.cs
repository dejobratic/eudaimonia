using Eudaimonia.Domain.Kernel;

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
        Assert.NotNull(Size.Small);
        Assert.NotNull(MathConstant.Pi);
    }

    [Fact]
    public void Constructor_WhenNameIsNotNull_CreatesInstanceWithProvidedName()
    {
        Assert.Equal("Small", Size.Small.Name);
        Assert.Equal("Pi", MathConstant.Pi.Name);
    }

    [Fact]
    public void Constructor_WhenNameIsNull_ThrowsException()
    {
        static StringConstant action() => StringConstant.Invalid;

        var exception = Assert.Throws<ArgumentNullException>((Func<StringConstant>)action);
        Assert.Equal("Value cannot be null. (Parameter 'name')", exception.Message);
    }

    [Fact]
    public void Constructor_WhenValueIsNotNull_CreatesInstanceWithProvidedValue()
    {
        Assert.Equal(1, Size.Small.Value);
        Assert.Equal(Math.PI, MathConstant.Pi.Value);
    }

    [Fact]
    public void Constructor_WhenValueIsNull_DoesNotThrowException()
    {
        Assert.NotNull(StringConstant.NullObject);
        Assert.Null(StringConstant.NullObject.Value);
    }

    [Fact]
    public void Equality_WhenComparingSameEnums_AreEqual()
    {
        Assert.Equal(Size.Small, Size.Small);
        Assert.Equal(MathConstant.Pi, MathConstant.Pi);
    }

    [Fact]
    public void ToString_Always_ReturnsName()
    {
        Assert.Equal("Small", Size.Small.ToString());
        Assert.Equal("Pi", MathConstant.Pi.ToString());
    }

    [Fact]
    public void ImplicitConversion_WhenValueIsSpecified_ConvertsToValueType()
    {
        int small = Size.Small;

        Assert.Equal(small, Size.Small.Value);
    }

    [Fact]
    public void GetAll_WithMemberInstances_ReturnsAll()
    {
        var items = Size.GetAll<Size>();

        Assert.Equivalent(new[] { Size.Small, Size.Medium, Size.Large }, items);
    }

    [Fact]
    public void GetAll_WithPropertyInstances_ReturnsAll()
    {
        var items = MathConstant.GetAll<MathConstant>();

        Assert.Equal(2, items.Count());
        Assert.Contains(MathConstant.Pi, items);
        Assert.Contains(MathConstant.E, items);
    }

    [Fact]
    public void FromValue_WhenExistingValueIsProvided_ReturnsMemberInstance()
    {
        Size instance = Size.FromValue<Size>(1);

        Assert.Equal(Size.Small, instance);
    }

    [Fact]
    public void FromValue_WhenExistingValueIsProvided_ReturnsPropertyInstance()
    {
        MathConstant instance = MathConstant.FromValue<MathConstant>(Math.PI);

        Assert.Equal(MathConstant.Pi, instance);
    }

    [Fact]
    public void FromValue_WhenNonexistentMemberValueIsProvided_ThrowsException()
    {
        static Size action() => Size.FromValue<Size>(0);

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Equal("'0' is not a valid name/value in Size enumeration.", exception.Message);
    }

    [Fact]
    public void FromValue_WhenNonexistentPropertyValueIsProvided_ThrowsException()
    {
        static MathConstant action() => MathConstant.FromValue<MathConstant>(2);

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Equal("'2' is not a valid name/value in MathConstant enumeration.", exception.Message);
    }

    [Fact]
    public void FromName_WhenNullNameIsProvided_ThrowsException()
    {
        static Size action() => Size.FromName<Size>(null!);

        var exception = Assert.Throws<ArgumentNullException>(action);
        Assert.Equal("Value cannot be null. (Parameter 'name')", exception.Message);
    }

    [Fact]
    public void FromName_WhenExistingMemberNameIsProvided_ReturnsMemberInstance()
    {
        Size instance = Size.FromName<Size>("Small");

        Assert.Equal(Size.Small, instance);
    }

    [Fact]
    public void FromName_WhenExistingPropertyNameIsProvided_ReturnsPropertyInstance()
    {
        MathConstant instance = MathConstant.FromName<MathConstant>("Pi");

        Assert.Equal(MathConstant.Pi, instance);
    }

    [Fact]
    public void FromName_WhenNonexistentMemberNameIsProvided_ThrowsException()
    {
        static Size action() => Size.FromName<Size>("ExtraSmall");

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Equal("'ExtraSmall' is not a valid name/value in Size enumeration.", exception.Message);
    }

    [Fact]
    public void FromName_WhenNonexistentPropertyNameIsProvided_ThrowsException()
    {
        static MathConstant action() => MathConstant.FromName<MathConstant>("i");

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Equal("'i' is not a valid name/value in MathConstant enumeration.", exception.Message);
    }
}