using SequelFilter.Resolvers;

namespace SequelFilter
{
    public static class ObjectFilter
    {
        public static bool Matches(object o, string filterExpression)
        {
            var filter = SequelFilterParser.Parse(filterExpression);
            return filter.Matches(o);
        }

        public static bool Matches(this ExecutableExpression filter, object o)
        {
            return filter(new SingleObjectResolver(o));
        }
    }
}
