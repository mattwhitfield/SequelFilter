using Irony.Parsing;
using SequelFilter.Comparison;

namespace SequelFilter.NodeTransforms
{
    public class BetweenExpressionTransform : ParseTreeTransform
    {
        public override string ApplicableTerm => SequelFilterGrammar.BetweenExpression;

        public override ExecutableExpression Transform(ParseTreeNode treeNode)
        {
            var targetValueFunc = GetFieldValueProvider(treeNode.ChildNodes[0]);
            var inverted = IsInverted(treeNode);
            var startIndex = inverted ? 3 : 2;
            var boundaryValue1Func = GetFieldValueProvider(treeNode.ChildNodes[startIndex]);
            var boundaryValue2Func = GetFieldValueProvider(treeNode.ChildNodes[startIndex + 2]);

            return x =>
            {
                var boundaryValue1 = boundaryValue1Func(x);
                var boundaryValue2 = boundaryValue2Func(x);

                object? min, max;
                if (ValueComparer.GreaterThan(boundaryValue2, boundaryValue1, ApplicableTerm))
                {
                    min = boundaryValue1;
                    max = boundaryValue2;
                }
                else
                {
                    min = boundaryValue2;
                    max = boundaryValue1;
                }

                var targetValue = targetValueFunc(x);

                var result = ValueComparer.GreaterThanEqualTo(targetValue, min, ApplicableTerm) && ValueComparer.LessThanEqualTo(targetValue, max, ApplicableTerm);

                return Invert(result, inverted);
            };
        }
    }
}
