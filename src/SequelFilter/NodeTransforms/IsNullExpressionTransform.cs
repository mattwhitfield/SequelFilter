using Irony.Parsing;
using System;

namespace SequelFilter.NodeTransforms
{
    public class IsNullExpressionTransform : ParseTreeTransform
    {
        public override string ApplicableTerm => SequelFilterGrammar.IsNullExpression;

        public override ExecutableExpression Transform(ParseTreeNode treeNode)
        {
            var operandFunc = GetFieldValueProvider(treeNode.ChildNodes[0]);
            var inverted = IsInverted(treeNode);

            return x =>
            {
                var operand = operandFunc(x);
                var result = operand == null || operand == DBNull.Value;
                return Invert(result, inverted);
            };
        }
    }
}
