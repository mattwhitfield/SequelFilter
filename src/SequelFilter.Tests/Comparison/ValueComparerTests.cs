namespace SequelFilter.Tests.Comparison
{
    using System;
    using FluentAssertions;
    using SequelFilter.Comparison;
    using Xunit;

    public static class ValueComparerTests
    {
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 1L, true)]
        [InlineData(1, "1", true)]
        [InlineData(1.0, "1", true)]
        [InlineData(1.0f, "1", true)]
        [InlineData("1", 1, true)]
        [InlineData(1, (uint)1, true)]
        [InlineData(null, 1, false)]
        [InlineData(1, null, false)]
        [InlineData(null, null, true)]
        public static void CanCallEquals(object left, object right, bool expectedResult)
        {
            // Arrange
            var operatorName = "TestValue202056200";

            // Act
            var result = ValueComparer.Equals(left, right, operatorName);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public static void CannotCallEqualsWithInvalidDesignTimeOperands()
        {
            // Arrange
            var operatorName = "TestValue202056200";

            // Act
            FluentActions.Invoking(() => ValueComparer.Equals(1, Guid.Empty, operatorName)).Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(1, "fred")]
        public static void CannotCallEqualsWithInvalidRuntimeOperands(object left, object right)
        {
            // Arrange
            var operatorName = "TestValue202056200";

            // Act
            FluentActions.Invoking(() => ValueComparer.Equals(left, right, operatorName)).Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(1, 1, false)]
        [InlineData(2, 1, true)]
        [InlineData(1, 2, false)]
        [InlineData("2", "1", true)]
        [InlineData(2, "1", true)]
        [InlineData("1", "2", false)]
        [InlineData(null, 1, false)]
        [InlineData(1, null, true)]
        public static void CanCallGreaterThan(object left, object right, bool expectedResult)
        {
            // Arrange
            var operatorName = "TestValue1044782118";

            // Act
            var result = ValueComparer.GreaterThan(left, right, operatorName);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(2, 1, true)]
        [InlineData(1, 2, false)]
        [InlineData("2", "1", true)]
        [InlineData(2, "1", true)]
        [InlineData("1", "2", false)]
        [InlineData(null, 1, false)]
        [InlineData(1, null, true)]
        public static void CanCallGreaterThanEqualTo(object left, object right, bool expectedResult)
        {
            // Arrange
            var operatorName = "TestValue264235161";

            // Act
            var result = ValueComparer.GreaterThanEqualTo(left, right, operatorName);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 1, false)]
        [InlineData(1, 1L, false)]
        [InlineData(1, "1", false)]
        [InlineData(1.0, "1", false)]
        [InlineData(1.0f, "1", false)]
        [InlineData(1, (uint)1, false)]
        [InlineData(null, 1, true)]
        [InlineData(1, null, true)]
        [InlineData(null, null, false)]
        public static void CanCallNotEquals(object left, object right, bool expectedResult)
        {
            // Arrange
            var operatorName = "TestValue1159886811";

            // Act
            var result = ValueComparer.NotEquals(left, right, operatorName);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 1, false)]
        [InlineData(2, 1, false)]
        [InlineData(1, 2, true)]
        [InlineData("2", "1", false)]
        [InlineData(2, "1", false)]
        [InlineData("1", "2", true)]
        [InlineData(null, 1, true)]
        [InlineData(1, null, false)]
        public static void CanCallLessThan(object left, object right, bool expectedResult)
        {
            // Arrange
            var operatorName = "TestValue225227504";

            // Act
            var result = ValueComparer.LessThan(left, right, operatorName);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(2, 1, false)]
        [InlineData(1, 2, true)]
        [InlineData("2", "1", false)]
        [InlineData(2, "1", false)]
        [InlineData("1", "2", true)]
        [InlineData(null, 1, true)]
        [InlineData(1, null, false)]
        public static void CanCallLessThanEqualTo(object left, object right, bool expectedResult)
        {
            // Arrange
            var operatorName = "TestValue1706080416";

            // Act
            var result = ValueComparer.LessThanEqualTo(left, right, operatorName);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}