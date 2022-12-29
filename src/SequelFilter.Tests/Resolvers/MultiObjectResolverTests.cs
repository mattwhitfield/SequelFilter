namespace SequelFilter.Tests.Resolvers
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using SequelFilter.Resolvers;
    using Xunit;

    public class MultiObjectResolverTests
    {
        private MultiObjectResolver _testClass;
        private IDictionary<string, object> _targetObjects;

        public MultiObjectResolverTests()
        {
            _targetObjects = new Dictionary<string, object> { { "us", Country.US }, { "uK", Country.UK } };
            _testClass = new MultiObjectResolver(_targetObjects);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new MultiObjectResolver(_targetObjects);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CanCallResolve()
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
        public void CanCallResolveWithInvalidCase()
        {
            // Arrange
            var names = new[] { "UK", "population" };
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