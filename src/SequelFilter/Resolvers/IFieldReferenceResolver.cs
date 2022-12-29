using System.Collections.Generic;

namespace SequelFilter.Resolvers
{
    public interface IFieldReferenceResolver
    {
        object? Resolve(IList<string> names, int startIndex);
    }
}
