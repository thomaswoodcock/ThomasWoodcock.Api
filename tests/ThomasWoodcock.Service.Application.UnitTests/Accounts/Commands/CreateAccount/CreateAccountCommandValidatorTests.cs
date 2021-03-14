using System;
using System.Linq;

using ThomasWoodcock.Service.Application.Accounts.Commands.CreateAccount;
using ThomasWoodcock.Service.Application.Common.Commands.Validation;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.FailureReasons;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Accounts.Commands.CreateAccount
{
    public sealed class CreateAccountCommandValidatorTests
    {
        public sealed class Validate : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public Validate(Fixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public void InvalidId_Validate_ReturnsFailedResult()
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Validate(new CreateAccountCommand(Guid.Empty, "Test Name",
                    "test@test.com", "TestPassword123"));

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);

                Assert.True(result.FailureReason is CommandValidationFailure validationFailure &&
                            validationFailure.Failures.First() is CommandPropertyValidationFailure
                            {
                                PropertyName: "Id"
                            });
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("NameThatIsLongerThanMaximumLength")]
            public void InvalidName_Validate_ReturnsFailedResult(string name)
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Validate(new CreateAccountCommand(
                    new Guid("C04759F1-8C88-451B-AE04-4DFE8112AE45"), name, "test@test.com", "TestPassword123"));

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);

                Assert.True(result.FailureReason is CommandValidationFailure validationFailure &&
                            validationFailure.Failures.First() is CommandPropertyValidationFailure
                            {
                                PropertyName: "Name"
                            });
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            [InlineData("test@test.")]
            [InlineData("www.test.com")]
            [InlineData("test^test.com")]
            public void InvalidEmailAddress_Validate_ReturnsFailedResult(string emailAddress)
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Validate(new CreateAccountCommand(
                    new Guid("C04759F1-8C88-451B-AE04-4DFE8112AE45"), "Test Name", emailAddress, "TestPassword123"));

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);

                Assert.True(result.FailureReason is CommandValidationFailure validationFailure &&
                            validationFailure.Failures.First() is CommandPropertyValidationFailure
                            {
                                PropertyName: "EmailAddress"
                            });
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("ShortPassword1")]
            [InlineData("PasswordLongerThanTheMaximumLength123")]
            public void InvalidPassword_Validate_ReturnsFailedResult(string password)
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Validate(new CreateAccountCommand(
                    new Guid("C04759F1-8C88-451B-AE04-4DFE8112AE45"), "Test Name", "test@test.com", password));

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);

                Assert.True(result.FailureReason is CommandValidationFailure validationFailure &&
                            validationFailure.Failures.First() is CommandPropertyValidationFailure
                            {
                                PropertyName: "Password"
                            });
            }

            [Fact]
            public void ValidCommand_Validate_ReturnsSuccessfulResult()
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Validate(new CreateAccountCommand(
                    new Guid("C04759F1-8C88-451B-AE04-4DFE8112AE45"), "Test Name", "test@test.com", "TestPassword123"));

                // Assert
                Assert.True(result.IsSuccessful);
                Assert.False(result.IsFailed);
                Assert.Null(result.FailureReason);
            }
        }

        public sealed class Fixture
        {
            public Fixture()
            {
                CreateAccountCommandValidatorConfiguration configuration = new();
                CommandValidatorBuilder<CreateAccountCommand> builder = new();
                configuration.Configure(builder);

                this.Sut = builder.Build();
            }

            internal CommandValidator<CreateAccountCommand> Sut { get; }
        }
    }
}
