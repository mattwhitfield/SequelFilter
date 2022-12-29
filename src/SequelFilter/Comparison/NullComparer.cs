using System;

namespace SequelFilter.Comparison
{
    public class NullComparer : ITypedComparer
    {
        private static Lazy<NullComparer> _instance = new Lazy<NullComparer>(() => new NullComparer());

        public static NullComparer Instance => _instance.Value;

        public bool Equals(object? left, object? right, string operatorName)
        {
            var leftIsNull = left == null;
            var rightIsNull = right == null;
            return leftIsNull == rightIsNull;
        }

        public bool GreaterThan(object? left, object? right, string operatorName)
        {
            var leftIsNull = left == null;
            var rightIsNull = right == null;
            return !leftIsNull && rightIsNull;
        }

        public bool GreaterThanEqualTo(object? left, object? right, string operatorName)
        {
            var leftIsNull = left == null;
            return !leftIsNull;
        }
    }
}
