using Irony.Parsing;
using SequelFilter.Comparison;
using System.Linq;

namespace SequelFilter.NodeTransforms
{
    public class InExpressionTransform : ParseTreeTransform
    {
        public override string ApplicableTerm => SequelFilterGrammar.InExpression;

        public override ExecutableExpression Transform(ParseTreeNode treeNode)
        {
            var targetValueFunc = GetFieldValueProvider(treeNode.ChildNodes[0]);
            var inverted = IsInverted(treeNode);
            var literalListNode = treeNode.ChildNodes[inverted ? 3 : 2];

            var literalFunctions = literalListNode.ChildNodes.Select(GetFieldValueProvider).ToList();

            return x =>
            {
                var targetValue = targetValueFunc(x);
                var targetValueInList = literalFunctions.Any(literalFunc => ValueComparer.Equals(literalFunc(x), targetValue, "IN"));

                return Invert(targetValueInList, inverted);
            };
        }
    }
}
