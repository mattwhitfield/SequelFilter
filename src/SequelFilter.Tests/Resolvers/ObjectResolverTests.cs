namespace SequelFilter.Tests.Resolvers
{
    using FluentAssertions;
    using SequelFilter.Resolvers;
    using Xunit;

    public static class ObjectResolverTests
    {
        [Fact]
        public static void CanCallResolve()
        {
            // Arrange
            var targetObject = new World();
            var names = new[] { "Statistics", "Population" };
            var startIndex = 0;

            // Act
            var result = ObjectResolver.Resolve(targetObject, names, startIndex);

            // Assert
            result.Should().Be(new RandomStatistics().Population);
        }

        [Fact]
        public static void CanCallResolveWithNonMatchingCase()
        {
            // Arrange
            var targetObject = new World();
            var names = new[] { "statistics", "pOpUlAtIoN" };
            var startIndex = 0;

            // Act
            var result = ObjectResolver.Resolve(targetObject, names, startIndex);

            // Assert
            result.Should().Be(new RandomStatistics().Population);
        }

        [Fact]
        public static void CanCallResolveWithInvalidNames()
        {
            // Arrange
            var targetObject = new World();
            var names = new[] { "Spandex", "StretchinessFactor" };
            var startIndex = 0;

            // Act
            var result = ObjectResolver.Resolve(targetObject, names, startIndex);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public static void CanCallResolveOnField()
        {
            // Arrange
            var targetObject = new World();
            var names = new[] { "Statistics", "FieldOfDreams" };
            var startIndex = 0;

            // Act
            var result = ObjectResolver.Resolve(targetObject, names, startIndex);

            // Assert
            result.Should().Be(new RandomStatistics().FieldOfDreams);
        }

        [Fact]
        public static void CanCallResolveWithNonZeroStartIndex()
        {
            // Arrange
            var targetObject = new World();
            var names = new[] { "Random", "Statistics", "Population" };
            var startIndex = 1;

            // Act
            var result = ObjectResolver.Resolve(targetObject, names, startIndex);

            // Assert
            result.Should().Be(new RandomStatistics().Population);
        }


        [Fact]
        public static void CanCallResolveForIntermediateObject()
        {
            // Arrange
            var targetObject = new World();
            var names = new[] { "Statistics" };
            var startIndex = 0;

            // Act
            var result = ObjectResolver.Resolve(targetObject, names, startIndex);

            // Assert
            result.Should().BeOfType<RandomStatistics>();
        }

        [Fact]
        public static void CanCallResolveOnNullObject()
        {
            // Arrange
            object? targetObject = null;
            var names = new[] { "Statistics", "Population" };
            var startIndex = 0;

            // Act
            var result = ObjectResolver.Resolve(targetObject, names, startIndex);

            // Assert
            result.Should().BeNull();
        }
    }
}