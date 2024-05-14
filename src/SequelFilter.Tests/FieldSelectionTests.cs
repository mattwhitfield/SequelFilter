namespace SequelFilter.Tests
{
    using FluentAssertions;
    using SequelFilter;
    using SequelFilter.Resolvers;
    using Xunit;

    public static class FieldSelectionTests
    {
        private static object[] TestCase(string filter, params object[] expectedValues)
        {
            return new object[] { filter, expectedValues };
        }

        public static IEnumerable<object[]> GetValueTestCases()
        {
            yield return TestCase($"Population.Int", Country.UK.Population.Int);
            yield return TestCase($"Population.Int, Population.Int", Country.UK.Population.Int, Country.UK.Population.Int);
            yield return TestCase($"Population.Int, Population.Double", Country.UK.Population.Int, Country.UK.Population.Double);
            yield return TestCase($"CountryName, Population.Double", Country.UK.CountryName, Country.UK.Population.Double);
            yield return TestCase($"CountryName, SpeaksEnglish, Population.Double", Country.UK.CountryName, Country.UK.SpeaksEnglish, Country.UK.Population.Double);
        }

        public static IEnumerable<object[]> GetNameTestCases()
        {
            yield return TestCase($"Population.Int", "Int");
            yield return TestCase($"Population.Int, Population.Int", "Int", "Int");
            yield return TestCase($"Population.Int, Population.Double", "Int", "Double");
            yield return TestCase($"CountryName, Population.Double", "CountryName", "Double");
            yield return TestCase($"CountryName, SpeaksEnglish, Population.Double", "CountryName", "SpeaksEnglish", "Double");
        }

        [Theory]
        [MemberData(nameof(GetValueTestCases))]
        public static void CanParseSelectors(string filterExpression, IEnumerable<object> expectedValues)
        {
            // Arrange
            var objectProvider = new SingleObjectResolver(Country.UK);

            // Act
            var result = SequelFilterParser.ParseSelectors(filterExpression).Select(x => x.ValueProvider(objectProvider));

            // Assert
            result.Should().BeEquivalentTo(expectedValues);
        }

        [Theory]
        [MemberData(nameof(GetNameTestCases))]
        public static void CanParseSelectorNames(string filterExpression, IEnumerable<object> expectedValues)
        {
            // Act
            var result = SequelFilterParser.ParseSelectors(filterExpression).Select(x => x.Name);

            // Assert
            result.Should().BeEquivalentTo(expectedValues);
        }
    }
}