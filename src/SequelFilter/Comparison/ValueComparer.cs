using System;

namespace SequelFilter.Comparison
{
    public static class ValueComparer
    {
        public static bool Equals(object? left, object? right, string operatorName) 
        {
            return ActOn(left, right, operatorName, x => x.Equals(left, right, operatorName));
        }

        public static bool GreaterThan(object? left, object? right, string operatorName)
        {
            return ActOn(left, right, operatorName, x => x.GreaterThan(left, right, operatorName));
        }

        public static bool GreaterThanEqualTo(object? left, object? right, string operatorName)
        {
            return ActOn(left, right, operatorName, x => x.GreaterThanEqualTo(left, right, operatorName));
        }

        private static bool ActOn(object? left, object? right, string operatorName, Func<ITypedComparer, bool> action)
        {
            var comparer = TypedComparerFactory.GetTypedComparer(left, right, operatorName);

            return action(comparer);
        }

        public static bool NotEquals(object? left, object? right, string operatorName)
        {
            return !Equals(left, right, operatorName);
        }

        public static bool LessThan(object? left, object? right, string operatorName)
        {
            return !GreaterThanEqualTo(left, right, operatorName);
        }

        public static bool LessThanEqualTo(object? left, object? right, string operatorName)
        {
            return !GreaterThan(left, right, operatorName);
        }
    }
}
