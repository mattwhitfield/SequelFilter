using Irony.Parsing;
using SequelFilter.Comparison;

namespace SequelFilter.NodeTransforms
{
    public class ComparisonExpressionTransform : ParseTreeTransform
    {
        public override string ApplicableTerm => SequelFilterGrammar.ComparisonExpression;

        public override ExecutableExpression Transform(ParseTreeNode treeNode)
        {
            var left = GetFieldValueProvider(treeNode.ChildNodes[0]);
            var right = GetFieldValueProvider(treeNode.ChildNodes[2]);
            var operatorSymbol = treeNode.ChildNodes[1].Term.Name;

            switch (operatorSymbol)
            {
                case SequelFilterGrammar.GreaterThanEqualTo_Symbol:
                    return x => ValueComparer.GreaterThanEqualTo(left(x), right(x), operatorSymbol);

                case SequelFilterGrammar.GreaterThan_Symbol:
                    return x => ValueComparer.GreaterThan(left(x), right(x), operatorSymbol);

                case SequelFilterGrammar.LessThanEqualTo_Symbol:
                    return x => ValueComparer.LessThanEqualTo(left(x), right(x), operatorSymbol);

                case SequelFilterGrammar.LessThan_Symbol:
                    return x => ValueComparer.LessThan(left(x), right(x), operatorSymbol);

                case SequelFilterGrammar.Not_Equal_Symbol:
                case SequelFilterGrammar.Not_Equals_Symbol:
                    return x => ValueComparer.NotEquals(left(x), right(x), operatorSymbol);

                default:
                    return x => ValueComparer.Equals(left(x), right(x), operatorSymbol);
            }
        }
    }
}
