using System;

using ThomasWoodcock.Service.Application.Common.Commands.Validation;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Common.Commands.Validation
{
    public sealed class CommandValidatorBuilderTests
    {
        public sealed class AddRule
        {
            [Fact]
            public void NullRule_AddRule_ThrowsArgumentNullException()
            {
                // Arrange
                CommandValidatorBuilder<CommandStub> sut = new();

                // Act Assert
                Assert.Throws<ArgumentNullException>(() => sut.AddRule(null));
            }
        }

        public sealed class Property
        {
            [Fact]
            public void NullPropertySelector_Property_ThrowsArgumentNullException()
            {
                // Arrange
                CommandValidatorBuilder<CommandStub> sut = new();

                // Act Assert
                Assert.Throws<ArgumentNullException>(() => sut.Property<string>(null));
            }
        }
    }
}
