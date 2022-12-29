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

        public object? Resolve(IList<string> names, int startIndex)
        {
            return ObjectResolver.Resolve(_targetObject, names, startIndex);
        }
    }
}
