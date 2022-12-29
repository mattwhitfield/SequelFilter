namespace SequelFilter.Tests.Comparison
{
    using FluentAssertions;
    using SequelFilter.Comparison;
    using Xunit;

    public static class TypedComparerCacheTests
    {
        [Fact]
        public static void CanCallCreateFor()
        {
            // Act
            var result = TypedComparerCache.CreateFor(typeof(string));

            // Assert
            result.Should().BeOfType<DefaultComparer<string>>();
        }

        [Fact]
        public static void CreateForCachesResult()
        {
            // Act
            var result = TypedComparerCache.CreateFor(typeof(int));
            var result2 = TypedComparerCache.CreateFor(typeof(int));

            // Assert
            result.Should().BeOfType<DefaultComparer<int>>();
            result2.Should().BeSameAs(result);
        }
    }
}