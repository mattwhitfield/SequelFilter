using System;
using System.Collections.Generic;

namespace SequelFilter.Comparison
{
    public static class TypedComparerCache
    {
        private static readonly Dictionary<Type, ITypedComparer> _comparerCache = new Dictionary<Type, ITypedComparer>();

        public static ITypedComparer CreateFor(Type type)
        {
            ITypedComparer comparer;

            lock (_comparerCache)
            {
                if (!_comparerCache.TryGetValue(type, out comparer))
                {
                    comparer = (ITypedComparer)Activator.CreateInstance(typeof(DefaultComparer<>).MakeGenericType(type));

                    _comparerCache[type] = comparer;
                }
            }

            return comparer;
        }
    }
}
