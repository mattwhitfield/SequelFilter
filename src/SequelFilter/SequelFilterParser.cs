namespace SequelFilter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Irony.Parsing;
    using SequelFilter.NodeTransforms;

    public static class SequelFilterParser
    {
        private static Lazy<LanguageData> _filterLanguageDataCacheInstance = new Lazy<LanguageData>(() => new LanguageData(new SequelFilterGrammar()));
        private static Lazy<LanguageData> _selectorLanguageDataCacheInstance = new Lazy<LanguageData>(() => new LanguageData(new FieldListGrammar()));

        public static ExecutableExpression Parse(string inputText)
        {
            return SequelFilterFactory.GetExecutableExpression(GetRoot(inputText, _filterLanguageDataCacheInstance));
        }

        public static IList<FieldSelector> ParseSelectors(string inputText)
        {
            var nodes = GetRoot(inputText, _selectorLanguageDataCacheInstance).ChildNodes;
            return nodes.Select(x => new FieldSelector (x.ChildNodes.Last().Token.ValueString, FieldReferenceTransform.Parse(x))).ToList();
        }

        private static ParseTreeNode GetRoot(string inputText, Lazy<LanguageData> language)
        {
            if (string.IsNullOrWhiteSpace(inputText))
            {
                throw new ArgumentNullException(nameof(inputText));
            }

            var tree = new Parser(language.Value).Parse(inputText);
            if (tree.Status == ParseTreeStatus.Error)
            {
                throw new ParseException(tree.ParserMessages.Select(x => x.ToErrorMessage()).Aggregate((x, y) => x + Environment.NewLine + y));
            }

            return tree.Root;
        }
    }
}
