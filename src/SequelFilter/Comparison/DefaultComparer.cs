using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace SequelFilter.Comparison
{
    public class DefaultComparer<T> : ITypedComparer
    {
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        private readonly TypeConverter _typeConverter = TypeDescriptor.GetConverter(typeof(T));

        private T ChangeType(object? o, string operatorName)
        {
            if (o is T typedValue)
            {
                return typedValue;
            }

            try
            {
                try
                {
                    return (T)Convert.ChangeType(o, typeof(T), CultureInfo.InvariantCulture);
                }
                catch (Exception ex) when (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
                { }

                return (T)_typeConverter.ConvertFromString(o?.ToString());
            }
            catch
            {
                throw new InvalidOperationException($"Could not change the type of the value '{o}' to type '{typeof(T).Name}' for operator '{operatorName}'.");
            }
        }

        private bool Apply(object? left, object? right, string operatorName, Func<int, bool> getResult)
        {
            var typedLeft = ChangeType(left, operatorName);
            var typedRight = ChangeType(right, operatorName);
            return getResult(_comparer.Compare(typedLeft, typedRight));
        }

        public bool Equals(object? left, object? right, string operatorName)
        {
            return Apply(left, right, operatorName, x => x == 0);
        }

        public bool GreaterThan(object? left, object? right, string operatorName)
        {
            return Apply(left, right, operatorName, x => x > 0);
        }

        public bool GreaterThanEqualTo(object? left, object? right, string operatorName)
        {
            return Apply(left, right, operatorName, x => x >= 0);
        }
    }
}
