using System;
using System.Collections.Generic;

namespace SequelFilter.Comparison
{
    public static class TypedComparerFactory 
    {
        static readonly Dictionary<Type, Type> _comparisonTypes = new Dictionary<Type, Type>
        {
            { typeof(bool), typeof(long) },
            { typeof(sbyte), typeof(long) },
            { typeof(char), typeof(long) },
            { typeof(int), typeof(long) },
            { typeof(long), typeof(long) },
            { typeof(short), typeof(long) },
            { typeof(byte), typeof(long) },
            { typeof(uint), typeof(long) },
            { typeof(ulong), typeof(long) },
            { typeof(ushort), typeof(long) },
            { typeof(DateTime), typeof(DateTimeOffset) },
            { typeof(DateTimeOffset), typeof(DateTimeOffset) },
            { typeof(decimal), typeof(decimal) },
            { typeof(double), typeof(decimal) },
            { typeof(float), typeof(decimal) },
            { typeof(Guid), typeof(Guid) },
            { typeof(string), typeof(string) },
            { typeof(TimeSpan), typeof(TimeSpan) },
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
                if (leftComparisonType == rightComparisonType || rightComparisonType == typeof(string))
                {
                    // make DefaultTypedComparer instance for leftComparisonType, cache it
                    return TypedComparerCache.CreateFor(leftComparisonType);
                }
                else if (leftComparisonType == typeof(string))
                {
                    // parse left as rightComparisonType
                    return TypedComparerCache.CreateFor(rightComparisonType);
                }
                else if ((leftComparisonType == typeof(decimal) && rightComparisonType == typeof(long)) ||
                         (leftComparisonType == typeof(long) && rightComparisonType == typeof(decimal)))
                {
                    return TypedComparerCache.CreateFor(typeof(decimal));
                }
            }

            throw new InvalidOperationException($"Could not find a comparer for types '{leftType.Name}' and '{rightType.Name}' for operator '{operatorName}'.");
        }
    }
}
