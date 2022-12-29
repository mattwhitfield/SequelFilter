using Irony.Parsing;

namespace SequelFilter.NodeTransforms
{
    public class FieldReferenceTransform : ParseTreeTransform
    {
        public override string ApplicableTerm => SequelFilterGrammar.FieldReference;

        public override ExecutableExpression Transform(ParseTreeNode treeNode)
        {
            var valueProvider = GetFieldValueProvider(treeNode);
            return x =>
            {
                var value = valueProvider(x);

                return value is bool b && b;
            };
        }
    }
}
