namespace SequelFilter.Tests
{
    using FluentAssertions;
    using Irony.Parsing;
    using SequelFilter;
    using Xunit;

    public class SequelFilterFactoryTests
    {
        [Fact]
        public void CannotCallGetExecutableExpressionWithInvalidTreeNode()
        {
            var treeNode = new ParseTreeNode(new Token(new Terminal("dave"), new SourceLocation(1, 2, 3), "dave", 4));

            FluentActions.Invoking(() => SequelFilterFactory.GetExecutableExpression(treeNode)).Should().Throw<ParseException>();
        }
    }
}