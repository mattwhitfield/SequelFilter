namespace SequelFilter.Tests.Resolvers
{
    using System.Collections.Generic;
    using FluentAssertions;
    using SequelFilter.Resolvers;
    using Xunit;

    public class ExtendedResolverTests
    {
        private ExtendedResolver _testClass;
        private string _declaredSymbol;
        private object _associatedValue;
        private IFieldReferenceResolver _fallbackResolver;

        public ExtendedResolverTests()
        {
            _declaredSymbol = "OldBlighty";
            _associatedValue = Country.UK;
            _fallbackResolver = new MultiObjectResolver(new Dictionary<string, object> { { "us", Country.US }, { "uK", Country.UK } });
            _testClass = new ExtendedResolver(_declaredSymbol, _associatedValue, _fallbackResolver);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ExtendedResolver(_declaredSymbol, _associatedValue, _fallbackResolver);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CanCallResolveOnFallback()
        {
            // Arrange
            var names = new[] { "us", "population" };
            var startIndex = 0;

            // Act
            var result = _testClass.Resolve(names, startIndex);

            // Assert
            result.Should().Be(Country.US.Population);
        }

        [Fact]
        public void CanCallResolveOnAssociatedObject()
        {
            // Arrange
            var names = new[] { "OldBlighty", "population" };
            var startIndex = 0;

            // Act
            var result = _testClass.Resolve(names, startIndex);

            // Assert
            result.Should().Be(Country.UK.Population);
        }

        [Fact]
        public void CanCallResolveWithInvalidCase()
        {
            // Arrange
            var names = new[] { "oldblighty", "population" };
            var startIndex = 0;

            // Act
            var result = _testClass.Resolve(names, startIndex);

            // Assert
            result.Should().Be(Country.UK.Population);
        }

        [Fact]
        public void CanCallResolveWithInvalidPropertyNames()
        {
            // Arrange
            var names = new[] { "dave", "cheese" };
            var startIndex = 0;

            // Act
            var result = _testClass.Resolve(names, startIndex);

            // Assert
            result.Should().BeNull();
        }
    }
}