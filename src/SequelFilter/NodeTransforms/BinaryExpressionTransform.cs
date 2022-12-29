using Irony.Parsing;

namespace SequelFilter.NodeTransforms
{
    public class BinaryExpressionTransform : ParseTreeTransform
    {
        public override string ApplicableTerm => SequelFilterGrammar.BinaryExpression;

        public override ExecutableExpression Transform(ParseTreeNode treeNode)
        {
            var left = SequelFilterFactory.GetExecutableExpression(treeNode.ChildNodes[0]);
            var right = SequelFilterFactory.GetExecutableExpression(treeNode.ChildNodes[2]);

            switch (treeNode.ChildNodes[1].Term.Name)
            {
                case SequelFilterGrammar.And:
                case SequelFilterGrammar.And_Symbol:
                    return x => left(x) && right(x);

                default:
                    return x => left(x) || right(x);
            }
        }
    }
}
