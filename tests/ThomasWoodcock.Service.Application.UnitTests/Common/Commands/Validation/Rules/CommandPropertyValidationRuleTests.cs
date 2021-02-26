using System;

using ThomasWoodcock.Service.Application.Common.Commands.FailureReasons;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.FailureReasons;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.Rules;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Common.Commands.Validation.Rules
{
    public sealed class CommandPropertyValidationRuleTests
    {
        public sealed class Constructor
        {
            [Fact]
            public void NullPropertySelector_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() =>
                    new CommandPropertyValidationRule<CommandStub, string>(null, _ => true, _ => "Test Message"));
            }

            [Fact]
            public void NullPredicate_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() =>
                    new CommandPropertyValidationRule<CommandStub, string>(command => command.TestProperty, null,
                        _ => "Test Message"));
            }

            [Fact]
            public void NullMessageBuilder_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() =>
                    new CommandPropertyValidationRule<CommandStub, string>(command => command.TestProperty, _ => true,
                        null));
            }

            [Fact]
            public void NullMessage_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() =>
                    new CommandPropertyValidationRule<CommandStub, string>(command => command.TestProperty, _ => true,
                        _ => null));
            }
        }

        public sealed class Check : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public Check(Fixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public void NullCommand_Check_ReturnsFailedResult()
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Check(null);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<InvalidCommandFailure>(result.FailureReason);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void InvalidProperty_Check_ReturnsFailedResult(string propertyValue)
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Check(new CommandStub { TestProperty = propertyValue });

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);

                Assert.True(result.FailureReason is CommandPropertyValidationFailure
                {
                    PropertyName: "TestProperty", Message: "'TestProperty' must have a value"
                });
            }

            [Fact]
            public void ValidProperty_Check_ReturnsSuccessfulResult()
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Check(new CommandStub { TestProperty = "Test Value" });

                // Assert
                Assert.True(result.IsSuccessful);
                Assert.False(result.IsFailed);
                Assert.Null(result.FailureReason);
            }
        }

        public sealed class Fixture
        {
            internal readonly CommandPropertyValidationRule<CommandStub, string> Sut =
                new(command => command.TestProperty, property => !string.IsNullOrWhiteSpace(property),
                    propertyName => $"'{propertyName}' must have a value");
        }
    }
}
