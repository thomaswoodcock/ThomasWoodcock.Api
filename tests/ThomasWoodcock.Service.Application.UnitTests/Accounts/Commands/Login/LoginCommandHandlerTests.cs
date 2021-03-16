using System;
using System.Threading.Tasks;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

using ThomasWoodcock.Service.Application.Accounts.Commands;
using ThomasWoodcock.Service.Application.Accounts.Commands.Login;
using ThomasWoodcock.Service.Application.Common.Commands.Validation;
using ThomasWoodcock.Service.Application.Common.Cryptography;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Domain.Accounts.FailureReasons;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Accounts.Commands.Login
{
    public sealed class LoginCommandHandlerTests
    {
        public sealed class HandleAsync : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public HandleAsync(Fixture fixture)
            {
                this._fixture = fixture;

                this._fixture.Validator.Validate(this._fixture.Command)
                    .Returns(Result.Success());

                this._fixture.Repository.GetAsync("test@test.com")
                    .Returns(this._fixture.Account);

                this._fixture.Hasher.Verify("HashedPassword", "TestPassword123")
                    .Returns(Result.Success());
            }

            [Fact]
            public async Task InvalidCommand_HandleAsync_ReturnsFailedResult()
            {
                // Arrange
                TestFailure failureReason = new();

                this._fixture.Validator.Validate(this._fixture.Command)
                    .Returns(Result.Failure(failureReason));

                // Act
                IResult result = await this._fixture.Sut.HandleAsync(this._fixture.Command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.Equal(failureReason, result.FailureReason);
            }

            [Fact]
            public async Task AccountDoesNotExist_HandleAsync_ReturnsFailedResult()
            {
                // Arrange
                this._fixture.Repository.GetAsync("test@test.com")
                    .ReturnsNull();

                // Act
                IResult result = await this._fixture.Sut.HandleAsync(this._fixture.Command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<AccountDoesNotExistFailure>(result.FailureReason);
            }

            [Fact]
            public async Task InvalidPassword_HandleAsync_ReturnsFailedResult()
            {
                // Arrange
                TestFailure failureReason = new();

                this._fixture.Hasher.Verify("HashedPassword", "TestPassword123")
                    .Returns(Result.Failure(failureReason));

                // Act
                IResult result = await this._fixture.Sut.HandleAsync(this._fixture.Command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<TestFailure>(result.FailureReason);
            }

            [Fact]
            public async Task ValidCommand_HandleAsync_ReturnsSuccessfulResult()
            {
                // Arrange Act
                IResult result = await this._fixture.Sut.HandleAsync(this._fixture.Command);

                // Assert
                Assert.True(result.IsSuccessful);
                Assert.False(result.IsFailed);
                Assert.Null(result.FailureReason);
            }

            private sealed class TestFailure : IFailureReason
            {
            }
        }

        public sealed class Fixture
        {
            internal readonly IPasswordHasher Hasher = Substitute.For<IPasswordHasher>();
            internal readonly IAccountCommandRepository Repository = Substitute.For<IAccountCommandRepository>();

            internal readonly ICommandValidator<LoginCommand> Validator =
                Substitute.For<ICommandValidator<LoginCommand>>();

            public Fixture()
            {
                this.Sut = new LoginCommandHandler(this.Validator, this.Repository, this.Hasher);
                this.Command = new LoginCommand("test@test.com", "TestPassword123");

                this.Account = Account.Create(new Guid("C8F52736-A492-4606-91BB-AC83241B26C0"), "Test Name",
                        "test@test.com", "HashedPassword")
                    .Value ?? throw new InvalidOperationException();
            }

            internal LoginCommandHandler Sut { get; }
            internal LoginCommand Command { get; }
            internal Account Account { get; }
        }
    }
}
