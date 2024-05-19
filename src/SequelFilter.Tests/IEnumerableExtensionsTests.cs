namespace SequelFilter.Tests
{
    using System;
    using FluentAssertions;
    using SequelFilter;
    using Xunit;

    public static class IEnumerableExtensionsTests
    {
        private static object[] TestCase(string filter, params Country[] expectedCountries)
        {
            return new object[] { filter, expectedCountries };
        }

        public static IEnumerable<object[]> GetTestCases()
        {
            //// Comparisons & Binary operators
            //yield return TestCase($"Population.Int < 100000000.0", Country.UK, Country.France);
            //yield return TestCase($"Population.Int < 100000000", Country.UK, Country.France);
            //yield return TestCase($"Population.Int < 100000000 && CountryName != '{Country.UK.CountryName}'", Country.France);
            //yield return TestCase($"Population.Int <= 100000000 || CountryName != '{Country.UK.CountryName}'", Country.France, Country.UK, Country.US);
            //yield return TestCase($"Population.Int > 100000000 && CountryName != '{Country.UK.CountryName}'", Country.US);
            //yield return TestCase($"Population.Int >= 100000000 && CountryName != '{Country.US.CountryName}'");
            //yield return TestCase($"Population.Double < 100000000.0", Country.UK, Country.France);
            //yield return TestCase($"Population.Double < 100000000", Country.UK, Country.France);
            //yield return TestCase($"Population.Double < 100000000 && CountryName != '{Country.UK.CountryName}'", Country.France);
            //yield return TestCase($"Population.Double <= 100000000 || CountryName != '{Country.UK.CountryName}'", Country.France, Country.UK, Country.US);
            //yield return TestCase($"Population.Double > 100000000 && CountryName != '{Country.UK.CountryName}'", Country.US);
            //yield return TestCase($"Population.Double >= 100000000 && CountryName != '{Country.US.CountryName}'");
            //yield return TestCase($"Population.Decimal < 100000000.0", Country.UK, Country.France);
            //yield return TestCase($"Population.Decimal < 100000000", Country.UK, Country.France);
            //yield return TestCase($"Population.Decimal < 100000000 && CountryName != '{Country.UK.CountryName}'", Country.France);
            //yield return TestCase($"Population.Decimal <= 100000000 || CountryName != '{Country.UK.CountryName}'", Country.France, Country.UK, Country.US);
            //yield return TestCase($"Population.Decimal > 100000000 && CountryName != '{Country.UK.CountryName}'", Country.US);
            //yield return TestCase($"Population.Decimal >= 100000000 && CountryName != '{Country.US.CountryName}'");
            //yield return TestCase($"Population.String < 100000000.0", Country.UK, Country.France);
            //yield return TestCase($"Population.String < 100000000", Country.UK, Country.France);
            //yield return TestCase($"Population.String < 100000000 && CountryName != '{Country.UK.CountryName}'", Country.France);
            //yield return TestCase($"Population.String <= 100000000 || CountryName != '{Country.UK.CountryName}'", Country.France, Country.UK, Country.US);
            //yield return TestCase($"Population.String > 100000000 && CountryName != '{Country.UK.CountryName}'", Country.US);
            //yield return TestCase($"Population.String >= 100000000 && CountryName != '{Country.US.CountryName}'");
            //yield return TestCase($"Population.Int == {Country.UK.Population.Int}", Country.UK);
            //yield return TestCase($"Population.Int != {Country.UK.Population.Int}", Country.US, Country.France);

            //// BETWEEN
            //yield return TestCase($"Population.Int BETWEEN {Country.France.Population.Int - 1} AND {Country.France.Population.Int + 1}", Country.France);
            //yield return TestCase($"Population.Int BETWEEN {Country.France.Population.Int + 1} AND {Country.France.Population.Int - 1}", Country.France);
            //yield return TestCase($"Population.Int NOT BETWEEN {Country.France.Population.Int - 1} AND {Country.France.Population.Int + 1}", Country.UK, Country.US);
            //yield return TestCase($"Population.Int NOT BETWEEN {Country.France.Population.Int + 1} AND {Country.France.Population.Int - 1}", Country.UK, Country.US);

            // Enumerable Expression
            yield return TestCase($"Subdivisions HAS_ANY x => x.RandomDecimal < 5", Country.UK);
            //yield return TestCase($"Subdivisions HAS_ANY x => x.RandomDouble < 5", Country.UK);
            //yield return TestCase($"Subdivisions HAS_ANY x => x.RandomDecimal > 5", Country.US, Country.France);
            //yield return TestCase($"Subdivisions HAS_ANY x => x.RandomDouble > 5", Country.US, Country.France);
            //yield return TestCase($"Words HAS_ANY x => x LIKE 'LEG%'", Country.UK, Country.US);
            //yield return TestCase($"Words HAS_ANY x => x LIKE 'PA%'", Country.UK, Country.US);
            //yield return TestCase($"Words HAS_SINGLE x => x LIKE 'RE%'", Country.France);
            //yield return TestCase($"Words HAS_NONE x => x LIKE 'N%'", Country.France, Country.US);

            //// Field Reference
            //yield return TestCase($"SpeaksEnglish", Country.UK, Country.US);
            //yield return TestCase($"NOT SpeaksEnglish", Country.France);

            //// IN
            //yield return TestCase($"Subdivisions HAS_ANY x => x.RandomFactor IN (1, 2, 3)", Country.UK);
            //yield return TestCase($"Subdivisions HAS_ANY x => x.RandomFactor IN (1, 2, 3, 12)", Country.UK, Country.US);

            //// IS NULL
            //yield return TestCase($"NullIfSpeaksEnglish IS NULL", Country.UK, Country.US);
            //yield return TestCase($"NullIfSpeaksEnglish IS NOT NULL", Country.France);

            //// Like
            //yield return TestCase($"CountryName LIKE 'U%'", Country.UK, Country.US);
            //yield return TestCase($"CountryName NOT LIKE 'U%'", Country.France);
            //yield return TestCase($"NullIfSpeaksEnglish LIKE 'U%'");

            //// NOT
            //yield return TestCase($"NOT Words HAS_SINGLE x => x LIKE 'RE%'", Country.UK, Country.US);
            //yield return TestCase($"NOT Words HAS_NONE x => x LIKE 'N%'", Country.UK);
        }

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public static void CanCallWhere(string filterExpression, IEnumerable<Country> expectedCountries)
        {
            // Act
            var result = new World().Countries.Where(filterExpression).ToList();

            // Assert
            result.Should().BeEquivalentTo(expectedCountries);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public static void CannotCallWhereWithEmptyFilterExpression(string value)
        {
            FluentActions.Invoking(() => new World().Countries.Where(value).ToList()).Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("this is probably not a valid filter")]
        [InlineData("Population !%$ 4999")]
        public static void CannotCallWhereWithInvalidFilterExpression(string value)
        {
            FluentActions.Invoking(() => new World().Countries.Where(value).ToList()).Should().Throw<ParseException>();
        }

        [Theory]
        [InlineData("Population HAS_ANY x => x LIKE 'LEG%'")]
        public static void CannotCallWhereWithInvalidRuntimeExpression(string value)
        {
            FluentActions.Invoking(() => new World().Countries.Where(value).ToList()).Should().Throw<InvalidOperationException>();
        }
    }
}