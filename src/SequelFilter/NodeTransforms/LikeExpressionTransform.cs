using Irony.Parsing;
using System.Text;
using System.Text.RegularExpressions;

namespace SequelFilter.NodeTransforms
{
    public class LikeExpressionTransform : ParseTreeTransform
    {
        public override string ApplicableTerm => SequelFilterGrammar.LikeExpression;

        public override ExecutableExpression Transform(ParseTreeNode treeNode)
        {
            var inverted = IsInverted(treeNode);
            var pattern = treeNode.ChildNodes[inverted ? 3 : 2].Token.ValueString;

            var regex = new Regex(ConvertPattern(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var left = GetFieldValueProvider(treeNode.ChildNodes[0]);

            return x =>
            {
                var value = left(x);
                if (value == null)
                {
                    return false;
                }

                var result = regex.IsMatch(value.ToString());
                return Invert(result, inverted);
            };
        }

        private static string ConvertPattern(string pattern)
        {
            var builder = new StringBuilder(Regex.Escape(pattern));

            builder.Insert(0, "^");
            builder.Append('$');

            return builder.Replace("%", ".*?").Replace("_", ".").Replace(@"\[", "[").Replace(@"\]", "]").Replace(@"\^", "^").ToString();
        }
    }
}
