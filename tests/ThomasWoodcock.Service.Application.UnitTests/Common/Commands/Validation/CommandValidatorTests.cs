using System.Linq;

using NSubstitute;

using ThomasWoodcock.Service.Application.Common.Commands.FailureReasons;
using ThomasWoodcock.Service.Application.Common.Commands.Validation;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.FailureReasons;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.Rules;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Domain.UnitTests.SharedKernel.Results.FailureReasons;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Common.Commands.Validation
{
    public sealed class CommandValidatorTests
    {
        public sealed class Validate : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public Validate(Fixture fixture)
            {
                this._fixture = fixture;

                this._fixture.FirstValidationRule.Check(Arg.Any<CommandStub>())
                    .Returns(Result.Success());

                this._fixture.SecondValidationRule.Check(Arg.Any<CommandStub>())
                    .Returns(Result.Success());
            }

            [Fact]
            public void NullCommand_Validate_ReturnsFailedResult()
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Validate(null);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<InvalidCommandFailure>(result.FailureReason);
            }

            [Fact]
            public void CommandFailsOneRule_Validate_ReturnsFailedResultWithOneValidationFailure()
            {
                // Arrange
                this._fixture.FirstValidationRule.Check(Arg.Any<CommandStub>())
                    .Returns(Result.Failure(new FailureReasonStub()));

                // Act
                IResult result = this._fixture.Sut.Validate(new CommandStub { TestProperty = "Test Value" });

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);

                Assert.True(result.FailureReason is CommandValidationFailure validationFailure &&
                            validationFailure.Failures.First() is FailureReasonStub);
            }

            [Fact]
            public void CommandFailsTwoRules_Validate_ReturnsFailedResultWithTwoValidationFailures()
            {
                // Arrange
                this._fixture.FirstValidationRule.Check(Arg.Any<CommandStub>())
                    .Returns(Result.Failure(new FailureReasonStub()));

                this._fixture.SecondValidationRule.Check(Arg.Any<CommandStub>())
                    .Returns(Result.Failure(new FailureReasonStub()));

                // Act
                IResult result = this._fixture.Sut.Validate(new CommandStub { TestProperty = "Test Value" });

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);

                Assert.True(result.FailureReason is CommandValidationFailure validationFailure &&
                            validationFailure.Failures.Count() == 2 &&
                            validationFailure.Failures.First() is FailureReasonStub &&
                            validationFailure.Failures.Last() is FailureReasonStub);
            }

            [Fact]
            public void ValidCommand_Validate_ReturnsSuccessfulResult()
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Validate(new CommandStub { TestProperty = "Test Value" });

                // Assert
                Assert.True(result.IsSuccessful);
                Assert.False(result.IsFailed);
                Assert.Null(result.FailureReason);
            }
        }

        public sealed class Fixture
        {
            internal readonly ICommandValidationRule<CommandStub> FirstValidationRule =
                Substitute.For<ICommandValidationRule<CommandStub>>();

            internal readonly ICommandValidationRule<CommandStub> SecondValidationRule =
                Substitute.For<ICommandValidationRule<CommandStub>>();

            public Fixture()
            {
                this.Sut = new CommandValidator<CommandStub>(new[]
                {
                    this.FirstValidationRule, this.SecondValidationRule
                });
            }

            internal CommandValidator<CommandStub> Sut { get; }
        }
    }
}
