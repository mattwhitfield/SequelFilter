namespace SequelFilter.Tests.Resolvers
{
    using FluentAssertions;
    using SequelFilter.Resolvers;
    using Xunit;

    public class SingleObjectResolverTests
    {
        private SingleObjectResolver _testClass;
        private object _targetObject;

        public SingleObjectResolverTests()
        {
            _targetObject = new World();
            _testClass = new SingleObjectResolver(_targetObject);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new SingleObjectResolver(_targetObject);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CanCallResolve()
        {
            // Arrange
            var names = new[] { "Statistics", "Population" };
            var startIndex = 0;

            // Act
            var result = _testClass.Resolve(names, startIndex);

            // Assert
            result.Should().Be(8007821437);
        }
    }
}