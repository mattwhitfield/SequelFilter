using System;
using System.Collections.Generic;

namespace SequelFilter.Comparison
{
    public static class TypedComparerFactory 
    {
        static readonly Dictionary<Type, (Type mainType, Type fallbackType)> _comparisonTypes = new Dictionary<Type, (Type mainType, Type fallbackType)>
        {
            { typeof(bool), (typeof(long), typeof(decimal)) },
            { typeof(sbyte), (typeof(long), typeof(decimal)) },
            { typeof(char), (typeof(long), typeof(decimal)) },
            { typeof(int), (typeof(long), typeof(decimal)) },
            { typeof(long), (typeof(long), typeof(decimal)) },
            { typeof(short), (typeof(long), typeof(decimal)) },
            { typeof(byte), (typeof(long), typeof(decimal)) },
            { typeof(uint), (typeof(long), typeof(decimal)) },
            { typeof(ulong), (typeof(long), typeof(decimal)) },
            { typeof(ushort), (typeof(long), typeof(decimal)) },
            { typeof(DateTime), (typeof(DateTimeOffset), typeof(DateTimeOffset)) },
            { typeof(DateTimeOffset), (typeof(DateTimeOffset), typeof(DateTimeOffset)) },
            { typeof(decimal), (typeof(decimal), typeof(decimal)) },
            { typeof(double), (typeof(decimal), typeof(decimal)) },
            { typeof(float), (typeof(decimal), typeof(decimal)) },
            { typeof(Guid), (typeof(Guid), typeof(Guid)) },
            { typeof(string), (typeof(string), typeof(string)) },
            { typeof(TimeSpan), (typeof(TimeSpan), typeof(TimeSpan)) },
        };

        public static ITypedComparer GetTypedComparer(object? left, object? right, string operatorName)
        {
            if (left == null || right == null)
            {
                return NullComparer.Instance;
            }

            var leftType = left.GetType();
            var rightType = right.GetType();

            if (leftType == rightType)
            {
                // make DefaultTypedComparer instance for leftType, cache it
                return TypedComparerCache.CreateFor(leftType);
            }

            if (_comparisonTypes.TryGetValue(leftType, out var leftComparisonType) &&
                _comparisonTypes.TryGetValue(rightType, out var rightComparisonType))
            {
                if (leftComparisonType == rightComparisonType)
                {
                    // make DefaultTypedComparer instance for leftComparisonType, cache it
                    return TypedComparerCache.CreateFor(leftComparisonType.mainType);
                }
                else if (rightComparisonType.mainType == typeof(string))
                {
                    // parse left as rightComparisonType
                    return TypedComparerCache.CreateFor(leftComparisonType.fallbackType);
                }
                else if (leftComparisonType.mainType == typeof(string))
                {
                    // parse left as rightComparisonType
                    return TypedComparerCache.CreateFor(rightComparisonType.fallbackType);
                }
                else if ((leftComparisonType.mainType == typeof(decimal) && rightComparisonType.mainType == typeof(long)) ||
                         (leftComparisonType.mainType == typeof(long) && rightComparisonType.mainType == typeof(decimal)))
                {
                    return TypedComparerCache.CreateFor(typeof(decimal));
                }
            }

            throw new InvalidOperationException($"Could not find a comparer for types '{leftType.Name}' and '{rightType.Name}' for operator '{operatorName}'.");
        }
    }
}
