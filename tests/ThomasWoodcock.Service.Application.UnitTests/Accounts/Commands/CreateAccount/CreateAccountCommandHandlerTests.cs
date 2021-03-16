using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NSubstitute;
using NSubstitute.ClearExtensions;

using ThomasWoodcock.Service.Application.Accounts.Commands;
using ThomasWoodcock.Service.Application.Accounts.Commands.CreateAccount;
using ThomasWoodcock.Service.Application.Common.Commands.Validation;
using ThomasWoodcock.Service.Application.Common.Cryptography;
using ThomasWoodcock.Service.Application.Common.DomainEvents;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Domain.Accounts.DomainEvents;
using ThomasWoodcock.Service.Domain.Accounts.FailureReasons;
using ThomasWoodcock.Service.Domain.SeedWork;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Accounts.Commands.CreateAccount
{
    public sealed class CreateAccountCommandHandlerTests
    {
        public sealed class HandleAsync : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public HandleAsync(Fixture fixture)
            {
                this._fixture = fixture;

                this._fixture.Validator.Validate(this._fixture.Command)
                    .Returns(Result.Success());

                this._fixture.Repository.ClearSubstitute();

                this._fixture.Hasher.Hash("TestPassword123")
                    .Returns("HashedPassword");

                this._fixture.Publisher.ClearSubstitute();
            }

            [Fact]
            public async Task InvalidCommand_HandleAsync_ReturnsFailedResult()
            {
                // Arrange
                this._fixture.Validator.Validate(this._fixture.Command)
                    .Returns(Result.Failure(new TestFailure()));

                // Act
                IResult result = await this._fixture.Sut.HandleAsync(this._fixture.Command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.IsType<TestFailure>(result.FailureReason);
            }

            [Fact]
            public async Task AccountWithExistingId_HandleAsync_ReturnsFailedResult()
            {
                // Arrange
                this._fixture.Repository.GetAsync(this._fixture.AccountId)
                    .Returns(this._fixture.Account);

                // Act
                IResult result = await this._fixture.Sut.HandleAsync(this._fixture.Command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.IsType<AccountIdExistsFailure>(result.FailureReason);
            }

            [Fact]
            public async Task AccountWithExistingEmailAddress_HandleAsync_ReturnsFailedResult()
            {
                // Arrange
                this._fixture.Repository.GetAsync("test@test.com")
                    .Returns(this._fixture.Account);

                // Act
                IResult result = await this._fixture.Sut.HandleAsync(this._fixture.Command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.IsType<AccountEmailExistsFailure>(result.FailureReason);
            }

            [Fact]
            public async Task EmptyAccountId_HandleAsync_ReturnsFailedResult()
            {
                // Arrange Act
                IResult result = await this._fixture.Sut.HandleAsync(new CreateAccountCommand(Guid.Empty, "Test Name",
                    "test@test.com", "TestPassword123"));

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<InvalidAccountIdFailure>(result.FailureReason);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public async Task NullEmptyOrWhiteSpaceAccountName_HandleAsync_ReturnsFailedResult(string accountName)
            {
                // Arrange Act
                IResult result = await this._fixture.Sut.HandleAsync(new CreateAccountCommand(this._fixture.AccountId,
                    accountName, "test@test.com", "TestPassword123"));

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<InvalidAccountNameFailure>(result.FailureReason);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            [InlineData("test@test.")]
            [InlineData("www.test.com")]
            [InlineData("test^test.com")]
            public async Task InvalidEmailAddress_HandleAsync_ReturnsFailedResult(string emailAddress)
            {
                // Arrange Act
                IResult result = await this._fixture.Sut.HandleAsync(new CreateAccountCommand(this._fixture.AccountId,
                    "Test Name", emailAddress, "TestPassword123"));

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<InvalidAccountEmailFailure>(result.FailureReason);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public async Task NullEmptyOrWhiteSpaceAccountPassword_HandleAsync_ReturnsFailedResult(
                string accountPassword)
            {
                // Arrange Act
                IResult result = await this._fixture.Sut.HandleAsync(new CreateAccountCommand(this._fixture.AccountId,
                    "Test Name", "test@test.com", accountPassword));

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<InvalidAccountPasswordFailure>(result.FailureReason);
            }

            [Fact]
            public async Task ValidCommand_HandleAsync_ReturnsSuccessfulResult()
            {
                // Arrange Act
                IResult result = await this._fixture.Sut.HandleAsync(this._fixture.Command);

                // Assert
                Assert.True(result.IsSuccessful);
            }

            [Fact]
            public async Task ValidCommand_HandleAsync_AddsAccountToRepository()
            {
                // Arrange Act
                IResult _ = await this._fixture.Sut.HandleAsync(this._fixture.Command);

                // Assert
                this._fixture.Repository.Received(1)
                    .Add(Arg.Is<Account>(account =>
                        account.Id == this._fixture.AccountId && account.Name == "Test Account" &&
                        account.EmailAddress.ToString() == "test@test.com" && account.Password == "HashedPassword"));

                await this._fixture.Repository.Received(1)
                    .SaveAsync();
            }

            [Fact]
            public async Task ValidCommand_HandleAsync_PublishesDomainEvent()
            {
                // Arrange Act
                IResult _ = await this._fixture.Sut.HandleAsync(this._fixture.Command);

                // Assert
                await this._fixture.Publisher.Received(1)
                    .PublishAsync(Arg.Is<IEnumerable<IDomainEvent>>(events => events.Any(e =>
                        e is AccountCreatedEvent && ((AccountCreatedEvent)e).Account.Id == this._fixture.AccountId)));
            }
        }

        private sealed class TestFailure : IFailureReason
        {
        }

        public sealed class Fixture
        {
            internal readonly Guid AccountId = new("019769F1-4197-4779-89C2-D764FE8AA8EB");
            internal readonly IPasswordHasher Hasher = Substitute.For<IPasswordHasher>();
            internal readonly IDomainEventPublisher Publisher = Substitute.For<IDomainEventPublisher>();

            internal readonly IAccountCommandRepository Repository = Substitute.For<IAccountCommandRepository>();

            internal readonly ICommandValidator<CreateAccountCommand> Validator =
                Substitute.For<ICommandValidator<CreateAccountCommand>>();

            public Fixture()
            {
                this.Sut = new CreateAccountCommandHandler(this.Validator, this.Repository, this.Hasher,
                    this.Publisher);

                this.Command =
                    new CreateAccountCommand(this.AccountId, "Test Account", "test@test.com", "TestPassword123");

                this.Account = Account.Create(this.AccountId, "Test Account", "test@test.com", "TestPassword123")
                    .Value ?? throw new InvalidOperationException();
            }

            internal CreateAccountCommandHandler Sut { get; }
            internal CreateAccountCommand Command { get; }
            internal Account Account { get; }
        }
    }
}
