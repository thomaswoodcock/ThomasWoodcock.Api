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
        public sealed class Constructor : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public Constructor(Fixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public void NullValidator_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() =>
                    new CreateAccountCommandHandler(null, this._fixture.Repository, this._fixture.Hasher,
                        this._fixture.Dispatcher));
            }

            [Fact]
            public void NullRepository_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() =>
                    new CreateAccountCommandHandler(this._fixture.Validator, null, this._fixture.Hasher,
                        this._fixture.Dispatcher));
            }

            [Fact]
            public void NullHasher_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => new CreateAccountCommandHandler(this._fixture.Validator,
                    this._fixture.Repository, null, this._fixture.Dispatcher));
            }

            [Fact]
            public void NullDispatcher_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() =>
                    new CreateAccountCommandHandler(this._fixture.Validator, this._fixture.Repository,
                        this._fixture.Hasher, null));
            }
        }

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

                this._fixture.Dispatcher.ClearSubstitute();
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
                IResult result = await this._fixture.Sut.HandleAsync(new CreateAccountCommand
                {
                    Id = Guid.Empty,
                    Name = "Test Name",
                    EmailAddress = "test@test.com",
                    Password = "TestPassword123"
                });

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
                IResult result = await this._fixture.Sut.HandleAsync(new CreateAccountCommand
                {
                    Id = this._fixture.AccountId,
                    Name = accountName,
                    EmailAddress = "test@test.com",
                    Password = "TestPassword123"
                });

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
                IResult result = await this._fixture.Sut.HandleAsync(new CreateAccountCommand
                {
                    Id = this._fixture.AccountId,
                    Name = "Test Name",
                    EmailAddress = emailAddress,
                    Password = "TestPassword123"
                });

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
                IResult result = await this._fixture.Sut.HandleAsync(new CreateAccountCommand
                {
                    Id = this._fixture.AccountId,
                    Name = "Test Name",
                    EmailAddress = "test@test.com",
                    Password = accountPassword
                });

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
            public async Task ValidCommand_HandleAsync_DispatchesDomainEvent()
            {
                // Arrange Act
                IResult _ = await this._fixture.Sut.HandleAsync(this._fixture.Command);

                // Assert
                await this._fixture.Dispatcher.Received(1)
                    .DispatchAsync(Arg.Is<IEnumerable<IDomainEvent>>(events => events.Any(e =>
                        e is AccountCreatedEvent && ((AccountCreatedEvent)e).Account.Id == this._fixture.AccountId)));
            }
        }

        private sealed class TestFailure : IFailureReason
        {
        }

        public sealed class Fixture
        {
            internal readonly Guid AccountId = new("019769F1-4197-4779-89C2-D764FE8AA8EB");
            internal readonly IDomainEventDispatcher Dispatcher = Substitute.For<IDomainEventDispatcher>();
            internal readonly IPasswordHasher Hasher = Substitute.For<IPasswordHasher>();

            internal readonly IAccountCommandRepository Repository = Substitute.For<IAccountCommandRepository>();

            internal readonly ICommandValidator<CreateAccountCommand> Validator =
                Substitute.For<ICommandValidator<CreateAccountCommand>>();

            public Fixture()
            {
                this.Sut = new CreateAccountCommandHandler(this.Validator, this.Repository, this.Hasher,
                    this.Dispatcher);

                this.Command = new CreateAccountCommand
                {
                    Id = this.AccountId,
                    Name = "Test Account",
                    EmailAddress = "test@test.com",
                    Password = "TestPassword123"
                };

                this.Account = Account.Create(this.AccountId, "Test Account", "test@test.com", "TestPassword123")
                    .Value;
            }

            internal CreateAccountCommandHandler Sut { get; }
            internal CreateAccountCommand Command { get; }
            internal Account Account { get; }
        }
    }
}
