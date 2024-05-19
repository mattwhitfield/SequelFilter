using System.Collections.Generic;
using System.Reflection;

namespace SequelFilter.Resolvers
{
    public static class ObjectResolver
    {
        public static object? Resolve(object? targetObject, IList<string> names, int startIndex)
        {
            if (targetObject == null)
            {
                return null;
            }

            var currentValue = GetObjectValue(targetObject, names[startIndex]);

            return ResolveFromValue(names, startIndex + 1, currentValue);
        }

        internal static object? ResolveFromValue(IList<string> names, int startIndex, object? currentValue)
        {
            // if this is the last item, just return the current value
            if (startIndex >= names.Count)
            {
                return currentValue;
            }
            else
            {
                return Resolve(currentValue, names, startIndex);
            }
        }

        private static object? GetObjectValue(object targetObject, string name)
        {
            var type = targetObject.GetType();
            var flags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var property = type.GetProperty(name, flags);
            if (property != null)
            {
                return property.GetValue(targetObject, null);
            }

            var field = type.GetField(name, flags);
            if (field != null)
            {
                return field.GetValue(targetObject);
            }

            return null;
        }
    }
}
