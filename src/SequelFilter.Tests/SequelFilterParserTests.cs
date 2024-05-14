namespace SequelFilter.Tests
{
    using System;
    using FluentAssertions;
    using SequelFilter;
    using Xunit;

    public static class SequelFilterParserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public static void CannotCallParseWithInvalidInputText(string value)
        {
            FluentActions.Invoking(() => SequelFilterParser.Parse(value)).Should().Throw<ArgumentNullException>().WithParameterName("inputText");
        }

        [Theory]
        [InlineData("1 == 1 == 1 == 1")]
        public static void CannotCallParseWithNonParsingInputText(string value)
        {
            FluentActions.Invoking(() => SequelFilterParser.Parse(value)).Should().Throw<ParseException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public static void CannotCallParseSelectorsWithInvalidInputText(string value)
        {
            FluentActions.Invoking(() => SequelFilterParser.ParseSelectors(value)).Should().Throw<ArgumentNullException>().WithParameterName("inputText");
        }

        [Theory]
        [InlineData("1 == 1 == 1 == 1")]
        public static void CannotCallParseSelectorsWithNonParsingInputText(string value)
        {
            FluentActions.Invoking(() => SequelFilterParser.ParseSelectors(value)).Should().Throw<ParseException>();
        }
    }
}