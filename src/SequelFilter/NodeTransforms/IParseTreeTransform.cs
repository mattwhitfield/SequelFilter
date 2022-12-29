using Irony.Parsing;

namespace SequelFilter.NodeTransforms
{
    public interface IParseTreeTransform
    {
        string ApplicableTerm { get; }

        ExecutableExpression Transform(ParseTreeNode treeNode);
    }
}
