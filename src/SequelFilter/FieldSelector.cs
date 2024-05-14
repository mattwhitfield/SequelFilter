using System;

namespace SequelFilter
{
    public class FieldSelector
    {
        public string Name { get; }

        public FieldValueProvider ValueProvider { get; }

        public FieldSelector(string name, FieldValueProvider valueProvider)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            ValueProvider = valueProvider ?? throw new ArgumentNullException(nameof(valueProvider));
            Name = name;
        }
    }
}
