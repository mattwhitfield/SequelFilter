using Irony.Parsing;
using SequelFilter.NodeTransforms;
using System;
using System.Collections.Generic;

namespace SequelFilter
{
    public static class SequelFilterFactory
    {
        private static readonly Dictionary<string, IParseTreeTransform> _transforms = new Dictionary<string, IParseTreeTransform>(StringComparer.OrdinalIgnoreCase);

        static SequelFilterFactory()
        {
            void RegisterTransform(IParseTreeTransform transform)
            {
                _transforms[transform.ApplicableTerm] = transform;
            }

            RegisterTransform(new BetweenExpressionTransform());
            RegisterTransform(new BinaryExpressionTransform());
            RegisterTransform(new ComparisonExpressionTransform());
            RegisterTransform(new EnumerableExpressionTransform());
            RegisterTransform(new FieldReferenceTransform());
            RegisterTransform(new InExpressionTransform());
            RegisterTransform(new IsNullExpressionTransform());
            RegisterTransform(new LikeExpressionTransform());
            RegisterTransform(new NotExpressionTransform());
        }

        public static ExecutableExpression GetExecutableExpression(ParseTreeNode treeNode)
        {
            if (_transforms.TryGetValue(treeNode.Term.Name, out var transform))
            {
                return transform.Transform(treeNode);
            }

            throw new ParseException($"Could not find a transform for the node type {treeNode.Term.Name}");
        }
    }
}
