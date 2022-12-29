using Irony.Parsing;
using System.Linq;

namespace SequelFilter.NodeTransforms
{
    public abstract class ParseTreeTransform : IParseTreeTransform
    {
        public abstract string ApplicableTerm { get; }

        public abstract ExecutableExpression Transform(ParseTreeNode treeNode);

        protected static FieldValueProvider GetFieldValueProvider(ParseTreeNode treeNode)
        {
            switch (treeNode.Term.Name)
            {
                case SequelFilterGrammar.NumberLiteral:
                    long l = (long)treeNode.Token.Value;
                    return _ => l;

                case SequelFilterGrammar.StringLiteral:
                    return _ => treeNode.Token.ValueString;

                case SequelFilterGrammar.FieldReference:
                    var values = treeNode.ChildNodes.Select(x => x.Token.ValueString).ToList();
                    return x => x.Resolve(values, 0);
            }

            throw new ParseException($"Cannot get a field value provider for the terminal type: {treeNode.Term.Name}");
        }

        protected static bool IsInverted(ParseTreeNode treeNode)
        {
            return treeNode.ChildNodes.Any(x => x.Term.Name == SequelFilterGrammar.Not);
        }

        protected static bool Invert(bool result, bool inverted)
        {
            if (inverted)
            {
                return !result;
            }

            return result;
        }
    }
}
