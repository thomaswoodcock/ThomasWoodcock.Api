using System;
using System.Threading.Tasks;

using NSubstitute;
using NSubstitute.ClearExtensions;

using ThomasWoodcock.Service.Application.Accounts.Commands;
using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Application.Accounts.EventHandlers;
using ThomasWoodcock.Service.Application.Accounts.Notifications;
using ThomasWoodcock.Service.Application.Common.Notifications;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Domain.Accounts.DomainEvents;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Accounts.EventHandlers
{
    public sealed class AccountCreatedEventHandlerTests
    {
        public sealed class Constructor
        {
            [Fact]
            public void NullRepository_Constructor_ThrowsArgumentNullException()
            {
                // Arrange
                var sender = Substitute.For<INotificationSender>();

                // Act Assert
                Assert.Throws<ArgumentNullException>(() => new AccountCreatedEventHandler(null, sender));
            }

            [Fact]
            public void NullSender_Constructor_ThrowsArgumentNullException()
            {
                // Arrange
                var repository = Substitute.For<IAccountActivationKeyRepository>();

                // Act Assert
                Assert.Throws<ArgumentNullException>(() => new AccountCreatedEventHandler(repository, null));
            }
        }

        public sealed class HandleAsync : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public HandleAsync(Fixture fixture)
            {
                this._fixture = fixture;

                this._fixture.Repository.ClearSubstitute();
                this._fixture.Sender.ClearSubstitute();
            }

            [Fact]
            public async Task NullEvent_HandleAsync_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => this._fixture.Sut.HandleAsync(null));
            }

            [Fact]
            public async Task ValidEvent_HandleAsync_AddsKeyToRepository()
            {
                // Arrange Act
                await this._fixture.Sut.HandleAsync(new AccountCreatedEvent(this._fixture.Account));

                // Assert
                this._fixture.Repository.Received(1)
                    .Add(this._fixture.Account, Arg.Is<AccountActivationKey>(key => key.Value != Guid.Empty));

                await this._fixture.Repository.Received(1)
                    .SaveAsync();
            }

            [Fact]
            public async Task ValidEvent_HandleAsync_SendsActivationNotification()
            {
                // Arrange Act
                await this._fixture.Sut.HandleAsync(new AccountCreatedEvent(this._fixture.Account));

                // Assert
                await this._fixture.Sender.Received(1)
                    .SendAsync(Arg.Is<AccountActivationNotification>(notification =>
                        notification.Account == this._fixture.Account &&
                        notification.ActivationKey.Value != Guid.Empty));
            }
        }

        public sealed class Fixture
        {
            internal readonly IAccountActivationKeyRepository Repository =
                Substitute.For<IAccountActivationKeyRepository>();

            internal readonly INotificationSender Sender = Substitute.For<INotificationSender>();

            public Fixture()
            {
                this.Sut = new AccountCreatedEventHandler(this.Repository, this.Sender);

                this.Account = Account.Create(new Guid("CC6D73DF-745B-4C36-BA5E-CDFA4CBEF2B0"), "Test Name",
                        "test@test.com", "TestPassword123")
                    .Value;
            }

            internal AccountCreatedEventHandler Sut { get; }
            internal Account Account { get; }
        }
    }
}
