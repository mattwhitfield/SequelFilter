using SequelFilter.Resolvers;

namespace SequelFilter
{
    public delegate bool ExecutableExpression(IFieldReferenceResolver fieldReferenceResolver);

    public delegate object? FieldValueProvider(IFieldReferenceResolver fieldReferenceResolver);
}
