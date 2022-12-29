using SequelFilter.Resolvers;
using System.Collections.Generic;

namespace SequelFilter
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, string filterExpression)
        {
            var filter = SequelFilterParser.Parse(filterExpression);
            return source.Where(filter);
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, ExecutableExpression filter)
        {
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
