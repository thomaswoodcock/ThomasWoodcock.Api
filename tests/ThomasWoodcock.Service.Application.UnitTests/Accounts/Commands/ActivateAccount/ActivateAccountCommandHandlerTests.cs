using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NSubstitute;
using NSubstitute.ClearExtensions;
using NSubstitute.ReturnsExtensions;

using ThomasWoodcock.Service.Application.Accounts.Commands;
using ThomasWoodcock.Service.Application.Accounts.Commands.ActivateAccount;
using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Application.Accounts.FailureReasons;
using ThomasWoodcock.Service.Application.Common.Commands.Validation;
using ThomasWoodcock.Service.Application.Common.DomainEvents;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Domain.Accounts.DomainEvents;
using ThomasWoodcock.Service.Domain.Accounts.FailureReasons;
using ThomasWoodcock.Service.Domain.SeedWork;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Accounts.Commands.ActivateAccount
{
    public sealed class ActivateAccountCommandHandlerTests
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
                Assert.Throws<ArgumentNullException>(() => new ActivateAccountCommandHandler(null,
                    this._fixture.AccountRepository, this._fixture.KeyRepository, this._fixture.Publisher));
            }

            [Fact]
            public void NullAccountRepository_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => new ActivateAccountCommandHandler(this._fixture.Validator,
                    null, this._fixture.KeyRepository, this._fixture.Publisher));
            }

            [Fact]
            public void NullKeyRepository_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => new ActivateAccountCommandHandler(this._fixture.Validator,
                    this._fixture.AccountRepository, null, this._fixture.Publisher));
            }

            [Fact]
            public void NullPublisher_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => new ActivateAccountCommandHandler(this._fixture.Validator,
                    this._fixture.AccountRepository, this._fixture.KeyRepository, null));
            }
        }

        public sealed class HandleAsync : IClassFixture<Fixture>
        {
            private readonly Account _account;
            private readonly ActivateAccountCommand _command;
            private readonly Fixture _fixture;

            public HandleAsync(Fixture fixture)
            {
                this._fixture = fixture;

                this._account = Account.Create(this._fixture.AccountId, "Test Name", "test@test.com", "TestPassword123")
                    .Value;

                this._command = new ActivateAccountCommand(this._fixture.AccountId, this._fixture.ActivationKey);

                this._fixture.Validator.Validate(this._command)
                    .Returns(Result.Success());

                this._fixture.AccountRepository.GetAsync(this._fixture.AccountId)
                    .Returns(this._account);

                this._fixture.KeyRepository.GetAsync(this._account)
                    .Returns(new AccountActivationKey(this._fixture.ActivationKey));

                this._fixture.Publisher.ClearSubstitute();
            }

            [Fact]
            public async Task InvalidCommand_HandleAsync_ReturnsFailedResult()
            {
                // Arrange
                TestFailure testFailure = new();

                this._fixture.Validator.Validate(this._command)
                    .Returns(Result.Failure(testFailure));

                // Act
                IResult result = await this._fixture.Sut.HandleAsync(this._command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.Equal(testFailure, result.FailureReason);
            }

            [Fact]
            public async Task NonExistentAccount_HandleAsync_ReturnsFailedResult()
            {
                // Arrange
                this._fixture.AccountRepository.GetAsync(this._command.AccountId)
                    .ReturnsNull();

                // Act
                IResult result = await this._fixture.Sut.HandleAsync(this._command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<AccountDoesNotExistFailure>(result.FailureReason);
            }

            [Fact]
            public async Task NonExistentActivationKey_HandleAsync_ReturnsFailedResult()
            {
                // Arrange
                this._fixture.KeyRepository.GetAsync(this._account)
                    .ReturnsNull();

                // Act
                IResult result = await this._fixture.Sut.HandleAsync(this._command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<ActivationKeyDoesNotExistFailure>(result.FailureReason);
            }

            [Fact]
            public async Task IncorrectActivationKey_HandleAsync_ReturnsFailedResult()
            {
                // Arrange
                this._fixture.KeyRepository.GetAsync(this._account)
                    .Returns(new AccountActivationKey(new Guid("61C40171-DB13-4CD2-8959-91C7E84A38D9")));

                // Act
                IResult result = await this._fixture.Sut.HandleAsync(this._command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<IncorrectActivationKeyFailure>(result.FailureReason);
            }

            [Fact]
            public async Task ActiveAccount_HandleAsync_ReturnsFailedResult()
            {
                // Arrange
                IResult _ = this._account.Activate();

                this._fixture.AccountRepository.GetAsync(this._fixture.AccountId)
                    .Returns(this._account);

                // Act
                IResult result = await this._fixture.Sut.HandleAsync(this._command);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<AccountAlreadyActiveFailure>(result.FailureReason);
            }

            [Fact]
            public async Task ValidCommand_HandleAsync_ReturnsSuccessfulResult()
            {
                // Arrange Act
                IResult result = await this._fixture.Sut.HandleAsync(this._command);

                // Assert
                Assert.True(result.IsSuccessful);
                Assert.False(result.IsFailed);
                Assert.Null(result.FailureReason);
            }

            [Fact]
            public async Task ValidCommand_HandleAsync_UpdatesAccountInRepository()
            {
                // Arrange Act
                IResult _ = await this._fixture.Sut.HandleAsync(this._command);

                // Assert
                Assert.True(this._account.IsActive);

                await this._fixture.AccountRepository.Received(1)
                    .SaveAsync();
            }

            [Fact]
            public async Task ValidCommand_HandleAsync_PublishesDomainEvent()
            {
                // Arrange Act
                IResult _ = await this._fixture.Sut.HandleAsync(this._command);

                // Assert
                await this._fixture.Publisher.Received(1)
                    .PublishAsync(Arg.Is<IEnumerable<IDomainEvent>>(events => events.Any(e =>
                        e is AccountActivatedEvent && ((AccountActivatedEvent)e).Account == this._account)));
            }
        }

        private sealed class TestFailure : IFailureReason
        {
        }

        public sealed class Fixture
        {
            internal readonly Guid AccountId = new("324A2ECA-1D1E-46FC-98A5-D4A668A07425");
            internal readonly IAccountCommandRepository AccountRepository = Substitute.For<IAccountCommandRepository>();
            internal readonly Guid ActivationKey = new("127C3DCC-0248-4A66-8094-5BFD25E8C874");

            internal readonly IAccountActivationKeyRepository KeyRepository =
                Substitute.For<IAccountActivationKeyRepository>();

            internal readonly IDomainEventPublisher Publisher = Substitute.For<IDomainEventPublisher>();

            internal readonly ICommandValidator<ActivateAccountCommand> Validator =
                Substitute.For<ICommandValidator<ActivateAccountCommand>>();

            public Fixture()
            {
                this.Sut = new ActivateAccountCommandHandler(this.Validator, this.AccountRepository, this.KeyRepository,
                    this.Publisher);
            }

            internal ActivateAccountCommandHandler Sut { get; }
        }
    }
}
