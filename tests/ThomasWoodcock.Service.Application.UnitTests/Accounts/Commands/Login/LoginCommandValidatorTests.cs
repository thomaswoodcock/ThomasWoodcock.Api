using System.Linq;

using ThomasWoodcock.Service.Application.Accounts.Commands.Login;
using ThomasWoodcock.Service.Application.Common.Commands.Validation;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.FailureReasons;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Accounts.Commands.Login
{
    public sealed class LoginCommandValidatorTests
    {
        public sealed class Validate : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public Validate(Fixture fixture)
            {
                this._fixture = fixture;
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
                IResult result = this._fixture.Sut.Validate(new LoginCommand(emailAddress, "TestPassword123"));

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
                IResult result = this._fixture.Sut.Validate(new LoginCommand("test@test.com", password));

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
                IResult result = this._fixture.Sut.Validate(new LoginCommand("test@test.com", "TestPassword123"));

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
                LoginCommandValidatorConfiguration configuration = new();
                CommandValidatorBuilder<LoginCommand> builder = new();
                configuration.Configure(builder);

                this.Sut = builder.Build();
            }

            internal CommandValidator<LoginCommand> Sut { get; }
        }
    }
}
