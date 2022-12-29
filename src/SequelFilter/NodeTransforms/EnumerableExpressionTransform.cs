using Irony.Parsing;
using SequelFilter.Resolvers;
using System;
using System.Collections;
using System.Linq;

namespace SequelFilter.NodeTransforms
{
    public class EnumerableExpressionTransform : ParseTreeTransform
    {
        private enum Mode
        {
            Any,
            None,
            Single,
        }

        public override string ApplicableTerm => SequelFilterGrammar.EnumerableExpression;

        public override ExecutableExpression Transform(ParseTreeNode treeNode)
        {
            var targetFunc = GetFieldValueProvider(treeNode.ChildNodes[0]);
            var operatorName = treeNode.ChildNodes[1].Term.Name;
            var declaredSymbol = treeNode.ChildNodes[2].Token.ValueString;
            var executableExpression = SequelFilterFactory.GetExecutableExpression(treeNode.ChildNodes[3]);

            var mode = Mode.Any;

            switch (operatorName)
            {
                case SequelFilterGrammar.HasNone:
                    mode = Mode.None;
                    break;

                case SequelFilterGrammar.HasSingle:
                    mode = Mode.Single;
                    break;
            }

            return x =>
            {
                var value = targetFunc(x);

                if (!(value is IEnumerable enumerable))
                {
                    var source = string.Join(".", treeNode.ChildNodes[0].ChildNodes.Select(x => x.Token.ValueString));
                    throw new InvalidOperationException($"Enumerable operators can only execute on enumerable nodes - '{source}' was not IEnumerable");
                }

                var matchCount = 0;
                foreach (object enumerableItem in enumerable)
                {
                    var extendedResolver = new ExtendedResolver(declaredSymbol, enumerableItem, x);
                    var isMatch = executableExpression(extendedResolver);
                    if (isMatch)
                    {
                        matchCount++;

                        if (mode == Mode.Any)
                        {
                            return true;
                        }
                        else if (mode == Mode.None)
                        {
                            return false;
                        }
                        else if (mode == Mode.Single && matchCount > 1)
                        {
                            return false;
                        }
                    }
                }

                if (mode == Mode.Any)
                {
                    return false;
                }
                else if (mode == Mode.None)
                {
                    return true;
                }
                else
                {
                    return matchCount == 1;
                }
            };
        }
    }
}
