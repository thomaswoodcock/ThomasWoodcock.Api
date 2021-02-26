using System;

using ThomasWoodcock.Service.Application.Common.Commands.Validation;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Common.Commands
{
    public sealed class CommandPropertyValidatorBuilderTests
    {
        public sealed class AddRule
        {
            [Fact]
            public void NullPredicate_AddRule_ThrowsArgumentNullException()
            {
                // Arrange
                CommandPropertyValidatorBuilder<CommandStub, string> sut =
                    new(new CommandValidatorBuilder<CommandStub>(), command => command.TestProperty);

                // Act Assert
                Assert.Throws<ArgumentNullException>(() => sut.AddRule(null, _ => "Test Message"));
            }

            [Fact]
            public void NullMessageBuilder_AddRule_ThrowsArgumentNullException()
            {
                // Arrange
                CommandPropertyValidatorBuilder<CommandStub, string> sut =
                    new(new CommandValidatorBuilder<CommandStub>(), command => command.TestProperty);

                // Act Assert
                Assert.Throws<ArgumentNullException>(() => sut.AddRule(_ => true, null));
            }
        }
    }
}
