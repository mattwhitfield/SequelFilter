using Irony.Parsing;

namespace SequelFilter.NodeTransforms
{
    public class NotExpressionTransform : ParseTreeTransform
    {
        public override string ApplicableTerm => SequelFilterGrammar.NotExpression;

        public override ExecutableExpression Transform(ParseTreeNode treeNode)
        {
            var operandFunc = SequelFilterFactory.GetExecutableExpression(treeNode.ChildNodes[1]);
            return x => !operandFunc(x);
        }
    }
}
