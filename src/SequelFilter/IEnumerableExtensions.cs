using SequelFilter.Resolvers;
using System.Collections.Generic;

namespace SequelFilter
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, string filterExpression)
        {
            var parseTree = SequelFilterParser.Parse(filterExpression);
            var filter = SequelFilterFactory.GetExecutableExpression(parseTree);
            foreach (var item in source)
            {
                if (item != null && filter(new SingleObjectResolver(item)))
                {
                    yield return item;
                }
            }
        }
    }
}
