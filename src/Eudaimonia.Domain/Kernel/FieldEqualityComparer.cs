using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Eudaimonia.Domain.Kernel;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "We use reflection to create Enumeration instance using FromName and FromValue.")]
internal class FieldEqualityComparer : IEqualityComparer
{
    private readonly Type _targetType;
    private readonly FieldInfo[] _fields;
    private readonly Func<object?, object?, bool> AllFieldsEqual;
    private readonly Func<object?, int> AllFieldsHashCode;

    private const BindingFlags AnyInstance
        = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

    public FieldEqualityComparer(Type target)
    {
        _targetType = target;
        _fields = GetFieldsRecursively().ToArray();
        AllFieldsEqual = AllFieldsEqualDelegate();
        AllFieldsHashCode = AllFieldsHashDelegate();
    }

    public new bool Equals(object? x, object? y)
        => AllFieldsEqual(x, y);

    public int GetHashCode(object obj)
        => AllFieldsHashCode(obj);

    private IEnumerable<FieldInfo> GetFieldsRecursively()
    {
        var type = _targetType;
        while (type != typeof(object))
        {
            foreach (var fieldInfo in type!.GetFields(AnyInstance))
                yield return fieldInfo;

            type = type.BaseType;
        }
    }

    private Func<object?, int> AllFieldsHashDelegate()
    {
        int multiplierValue = 1;
        Expression body = Expression.Constant(_targetType.GetHashCode(), typeof(int));
        ParameterExpression param = Expression.Parameter(typeof(object));

        foreach (var fi in _fields)
        {
            Expression left = Expression.Field(
                Expression.Convert(param, fi.DeclaringType!), fi);

            // This is may not be specific enough!
            MethodInfo getHashCodeMethodInfo = fi.FieldType
                .GetMethod("GetHashCode", Array.Empty<Type>())!;

            Expression hashCode = Expression.Call(left, getHashCodeMethodInfo);

            Expression multiplier = Expression.Constant(multiplierValue);
            Expression multipliedHashCode = Expression.Multiply(hashCode, multiplier);
            multiplierValue *= 17;

            body = Expression.ExclusiveOr(body, multipliedHashCode);
        }

        var lambda = Expression.Lambda(body, param);
        return (Func<object?, int>)lambda.Compile();
    }

    private Func<object?, object?, bool> AllFieldsEqualDelegate()
    {
        var param1 = Expression.Parameter(typeof(object));
        var param2 = Expression.Parameter(typeof(object));
        Expression body = null!;

        if (_fields.Length == 0)
        {
            body = Expression.Constant(true);
        }
        else
        {
            foreach (var fi in _fields)
            {
                Expression left = Expression.Field(
                    Expression.Convert(param1, fi.DeclaringType!), fi);

                if (!fi.FieldType.IsClass)
                    left = Expression.Convert(left, typeof(object));

                Expression right = Expression.Field(
                    Expression.Convert(param2, fi.DeclaringType!), fi);

                if (!fi.FieldType.IsClass)
                    right = Expression.Convert(right, typeof(object));

                Expression equals = Expression.Call(ObjectEquals, left, right);

                body = (body is null) ? equals : Expression.AndAlso(body, equals);
            }
        }

        var lambda = Expression.Lambda(body, param1, param2);
        return (Func<object?, object?, bool>)lambda.Compile();
    }

    private static MethodInfo ObjectEquals
        => typeof(object).GetMethod("Equals", new Type[] { typeof(object), typeof(object) })!;
}