using System;
using System.Linq;
using System.Threading.Tasks;

using NSubstitute;

using ThomasWoodcock.Service.Application.Common.Commands;
using ThomasWoodcock.Service.Application.Common.Commands.FailureReasons;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Domain.UnitTests.SharedKernel.Results.FailureReasons;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Common.Commands
{
    public sealed class CommandSenderTests
    {
        public sealed class Constructor
        {
            [Fact]
            public void NullHandlers_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => new CommandSender(null));
            }
        }

        public sealed class SendAsync
        {
            [Fact]
            public async Task NullCommand_SendAsync_ThrowsArgumentNullException()
            {
                // Arrange
                CommandSender sut = new(Enumerable.Empty<ICommandHandler>());

                // Act Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => sut.SendAsync(null));
            }

            [Fact]
            public async Task CommandWithNoHandler_SendAsync_ReturnsFailedResult()
            {
                // Arrange
                CommandStub command = new();
                CommandSender sut = new(Enumerable.Empty<ICommandHandler>());

                // Act
                IResult result = await sut.SendAsync(command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<UnhandledCommandFailure>(result.FailureReason);
            }

            [Fact]
            public async Task CommandWithOneHandler_SendAsync_SendsCommandToHandler()
            {
                // Arrange
                CommandStub command = new();
                FailureReasonStub failureReason = new();

                ICommandHandler commandHandler = Substitute.For<ICommandHandler<CommandStub>>();

                commandHandler.HandleAsync(command)
                    .Returns(Result.Failure(failureReason));

                CommandSender sut = new(new[] { commandHandler });

                // Act
                IResult result = await sut.SendAsync(command);

                // Assert
                Assert.Equal(failureReason, result.FailureReason);
            }

            [Fact]
            public async Task CommandWithMultipleHandlers_SendAsync_SendsCommandToFirstHandler()
            {
                // Arrange
                CommandStub command = new();
                FailureReasonStub firstFailure = new();
                FailureReasonStub secondFailure = new();

                ICommandHandler firstHandler = Substitute.For<ICommandHandler<CommandStub>>();
                ICommandHandler secondHandler = Substitute.For<ICommandHandler<CommandStub>>();

                firstHandler.HandleAsync(command)
                    .Returns(Result.Failure(firstFailure));

                secondHandler.HandleAsync(command)
                    .Returns(Result.Failure(secondFailure));

                CommandSender sut = new(new[] { firstHandler, secondHandler });

                // Act
                IResult result = await sut.SendAsync(command);

                // Assert
                Assert.Equal(firstFailure, result.FailureReason);
            }
        }
    }
}
