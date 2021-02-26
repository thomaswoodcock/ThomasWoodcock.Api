using System;

using ThomasWoodcock.Service.Application.Common.Commands.Validation.FailureReasons;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Common.Commands.Validation.FailureReasons
{
    public sealed class CommandPropertyValidationFailureTests
    {
        public sealed class Constructor
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void NullEmptyOrWhiteSpacePropertyName_Constructor_ThrowsArgumentNullException(string propertyName)
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() =>
                    new CommandPropertyValidationFailure(propertyName, "Test Message"));
            }

            [Fact]
            public void ValidArguments_Constructor_SetsProperties()
            {
                // Arrange Act
                CommandPropertyValidationFailure sut = new("TestProperty", "Test Message");

                // Assert
                Assert.Equal("TestProperty", sut.PropertyName);
                Assert.Equal("Test Message", sut.Message);
            }
        }
    }
}
