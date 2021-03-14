using System;
using System.Linq;

using ThomasWoodcock.Service.Application.Accounts.Commands.ActivateAccount;
using ThomasWoodcock.Service.Application.Common.Commands.Validation;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.FailureReasons;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Accounts.Commands.ActivateAccount
{
    public sealed class ActivateAccountCommandValidatorTests
    {
        public sealed class Validate : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public Validate(Fixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public void InvalidAccountId_Validate_ReturnsFailedResult()
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Validate(new ActivateAccountCommand(Guid.Empty,
                    new Guid("9F0DC9EB-7363-4408-9F48-B224444E48EF")));

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);

                Assert.True(result.FailureReason is CommandValidationFailure validationFailure &&
                            validationFailure.Failures.First() is CommandPropertyValidationFailure
                            {
                                PropertyName: "AccountId"
                            });
            }

            [Fact]
            public void InvalidActivationKey_Validate_ReturnsFailedResult()
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Validate(
                    new ActivateAccountCommand(new Guid("7D591461-3996-4BDA-9ECD-9C7F2C0F6CAE"), Guid.Empty));

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);

                Assert.True(result.FailureReason is CommandValidationFailure validationFailure &&
                            validationFailure.Failures.First() is CommandPropertyValidationFailure
                            {
                                PropertyName: "ActivationKey"
                            });
            }

            [Fact]
            public void ValidCommand_Validate_ReturnsSuccessfulResult()
            {
                // Arrange Act
                IResult result = this._fixture.Sut.Validate(new ActivateAccountCommand(
                    new Guid("7D591461-3996-4BDA-9ECD-9C7F2C0F6CAE"),
                    new Guid("9F0DC9EB-7363-4408-9F48-B224444E48EF")));

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
                ActivateAccountCommandValidatorConfiguration configuration = new();
                CommandValidatorBuilder<ActivateAccountCommand> builder = new();
                configuration.Configure(builder);

                this.Sut = builder.Build();
            }

            internal CommandValidator<ActivateAccountCommand> Sut { get; }
        }
    }
}
