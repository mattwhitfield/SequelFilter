using System;
using System.Collections.Generic;

namespace SequelFilter.Resolvers
{
    public class MultiObjectResolver : IFieldReferenceResolver
    {
        private readonly IDictionary<string, object> _targetObjects;

        public MultiObjectResolver(IDictionary<string, object> targetObjects)
        {
            _targetObjects = new Dictionary<string, object>(targetObjects, StringComparer.OrdinalIgnoreCase);
        }

        public object? Resolve(IList<string> names, int startIndex, object? from = null)
        {
            if (!_targetObjects.TryGetValue(names[startIndex], out var currentValue))
            {
                if (from != null)
                {
                    return ObjectResolver.ResolveFromValue(names, startIndex, from);
                }

                return null;
            }

            return ObjectResolver.ResolveFromValue(names, startIndex, currentValue);
        }
    }
}
