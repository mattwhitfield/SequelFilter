namespace SequelFilter
{
    using System;
    using System.Linq;
    using Irony.Parsing;

    public static class SequelFilterParser
    {
        private static Lazy<LanguageData> _languageDataCacheInstance = new Lazy<LanguageData>(() => new LanguageData(new SequelFilterGrammar()));

        public static ParseTreeNode Parse(string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
            {
                throw new ArgumentNullException(nameof(inputText));
            }

            var tree = new Parser(_languageDataCacheInstance.Value).Parse(inputText);
            if (tree.Status == ParseTreeStatus.Error)
            {
                throw new ParseException(tree.ParserMessages.Select(x => x.ToErrorMessage()).Aggregate((x, y) => x + Environment.NewLine + y));
            }

            return tree.Root;
        }
    }
}
