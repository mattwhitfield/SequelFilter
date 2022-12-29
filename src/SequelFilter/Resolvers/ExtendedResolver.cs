using System;
using System.Collections.Generic;

namespace SequelFilter.Resolvers
{
    public class ExtendedResolver : IFieldReferenceResolver
    {
        private readonly string _declaredSymbol;
        private readonly object _associatedValue;
        private readonly IFieldReferenceResolver _fallbackResolver;

        public ExtendedResolver(string declaredSymbol, object associatedValue, IFieldReferenceResolver fallbackResolver)
        {
            _declaredSymbol = declaredSymbol;
            _associatedValue = associatedValue;
            _fallbackResolver = fallbackResolver;
        }

        public object? Resolve(IList<string> names, int startIndex)
        {
            var symbol = names[startIndex];

            // if the first part is looking for our declared symbol
            if (string.Equals(symbol, _declaredSymbol, StringComparison.OrdinalIgnoreCase))
            {
                var currentValue = _associatedValue;

                return ObjectResolver.ResolveFromValue(names, startIndex, currentValue);
            }

            return _fallbackResolver.Resolve(names, startIndex);
        }
    }
}
