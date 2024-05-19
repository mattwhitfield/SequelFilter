using System.Collections.Generic;

namespace SequelFilter.Resolvers
{
    public class SingleObjectResolver : IFieldReferenceResolver
    {
        private readonly object _targetObject;

        public SingleObjectResolver(object targetObject)
        {
            _targetObject = targetObject;
        }

        public object? Resolve(IList<string> names, int startIndex, object? from = null)
        {
            return ObjectResolver.Resolve(from ?? _targetObject, names, startIndex);
        }
    }
}
