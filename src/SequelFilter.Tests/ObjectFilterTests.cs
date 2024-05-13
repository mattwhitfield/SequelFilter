namespace SequelFilter.Tests
{
    using System;
    using FluentAssertions;
    using SequelFilter;
    using Xunit;

    public static class ObjectFilterTests
    {
        public static IEnumerable<object[]> GetTestCases()
        {
            var allCountries = new World().Countries;

            foreach (var testCase in IEnumerableExtensionsTests.GetTestCases())
            {
                string filterExpression = (string)testCase[0];
                IList<Country> matchingCountries = (IList<Country>)testCase[1];
                IList<Country> nonMatchingCountries = allCountries.Where(x => !matchingCountries.Contains(x)).ToList();
                yield return new object[] { filterExpression, matchingCountries, nonMatchingCountries };
            }
        }

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public static void CanCallMatches(string filterExpression, IEnumerable<Country> matchingCountries, IEnumerable<Country> nonMatchingCountries)
        {
            // Arrange
            var filter = SequelFilterParser.Parse(filterExpression);

            // Assert
            foreach (var matching in matchingCountries)
            {
                filter.Matches(matching).Should().BeTrue();
            }

            foreach (var matching in nonMatchingCountries)
            {
                filter.Matches(matching).Should().BeFalse();
            }
        }

        [Fact]
        public static void CanCallMatchesWithExpression()
        {
            // Arrange
            var country = Country.UK;

            // Act
            var result = ObjectFilter.Matches(country, $"Population.Int = {country.Population.Int}");

            // Assert
            result.Should().BeTrue();
        }
   }
}