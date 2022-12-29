namespace SequelFilter.Tests.NodeTransforms
{
    using FluentAssertions;
    using Irony.Parsing;
    using SequelFilter;
    using SequelFilter.NodeTransforms;
    using Xunit;

    public class ParseTreeTransformTests
    {
        private class TestParseTreeTransform : ParseTreeTransform
        {
            public static FieldValueProvider PublicGetFieldValueProvider(ParseTreeNode treeNode)
            {
                return GetFieldValueProvider(treeNode);
            }

            public override ExecutableExpression Transform(ParseTreeNode treeNode)
            {
                return _ => false;
            }

            public override string ApplicableTerm => "Fred";
        }

        [Fact]
        public void GetFieldValueProviderOnInvalidTypeThrows()
        {
            // Arrange
            var treeNode = new ParseTreeNode(new Token(new Terminal("TestValue729508864"), new SourceLocation(), "TestValue367685209", new object()));

            // Act
            FluentActions.Invoking(() => TestParseTreeTransform.PublicGetFieldValueProvider(treeNode)).Should().Throw<ParseException>();
        }
    }
}