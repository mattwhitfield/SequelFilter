namespace SequelFilter.Tests
{
    using System;
    using FluentAssertions;
    using SequelFilter;
    using Xunit;

    public class FieldSelectorTests
    {
        private FieldSelector _testClass;
        private string _name;
        private FieldValueProvider _valueProvider;

        public FieldSelectorTests()
        {
            _name = "TestValue230327852";
            _valueProvider = x => new object();
            _testClass = new FieldSelector(_name, _valueProvider);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new FieldSelector(_name, _valueProvider);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CannotConstructWithNullValueProvider()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = FluentActions.Invoking(() => new FieldSelector(_name, null)).Should().Throw<ArgumentNullException>().WithParameterName("valueProvider");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotConstructWithInvalidName(string value)
        {
            FluentActions.Invoking(() => new FieldSelector(value, _valueProvider)).Should().Throw<ArgumentNullException>().WithParameterName("name");
        }

        [Fact]
        public void NameIsInitializedCorrectly()
        {
            _testClass.Name.Should().Be(_name);
        }

        [Fact]
        public void ValueProviderIsInitializedCorrectly()
        {
            _testClass.ValueProvider.Should().BeSameAs(_valueProvider);
        }
    }
}