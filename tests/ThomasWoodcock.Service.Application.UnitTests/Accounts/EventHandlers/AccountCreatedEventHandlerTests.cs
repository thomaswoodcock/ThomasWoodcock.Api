using System;
using System.Threading.Tasks;

using NSubstitute;
using NSubstitute.ClearExtensions;

using ThomasWoodcock.Service.Application.Accounts.EventHandlers;
using ThomasWoodcock.Service.Application.Accounts.Notifications;
using ThomasWoodcock.Service.Application.Accounts.Services;
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
            public void NullKeyGenerator_Constructor_ThrowsArgumentNullException()
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
                var keyGenerator = Substitute.For<IAccountActivationKeyGenerator>();

                // Act Assert
                Assert.Throws<ArgumentNullException>(() => new AccountCreatedEventHandler(keyGenerator, null));
            }
        }

        public sealed class HandleAsync : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public HandleAsync(Fixture fixture)
            {
                this._fixture = fixture;

                this._fixture.KeyGenerator.ClearSubstitute();
                this._fixture.Sender.ClearSubstitute();
            }

            [Fact]
            public async Task NullEvent_HandleAsync_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => this._fixture.Sut.HandleAsync(null));
            }

            [Fact]
            public async Task ValidEvent_HandleAsync_SendsActivationNotification()
            {
                // Arrange
                Account account = Account.Create(new Guid("CC6D73DF-745B-4C36-BA5E-CDFA4CBEF2B0"), "Test Name",
                        "test@test.com", "TestPassword123")
                    .Value;

                Guid activationKey = new("1B9CACF7-14E3-465C-B181-554D666A970C");

                this._fixture.KeyGenerator.GenerateAsync(account)
                    .Returns(activationKey);

                // Act
                await this._fixture.Sut.HandleAsync(new AccountCreatedEvent(account));

                // Assert
                await this._fixture.Sender.Received(1)
                    .SendAsync(Arg.Is<AccountActivationNotification>(notification =>
                        notification.Account == account && notification.ActivationKey == activationKey));
            }
        }

        public sealed class Fixture
        {
            internal readonly IAccountActivationKeyGenerator KeyGenerator =
                Substitute.For<IAccountActivationKeyGenerator>();

            internal readonly INotificationSender Sender = Substitute.For<INotificationSender>();

            public Fixture()
            {
                this.Sut = new AccountCreatedEventHandler(this.KeyGenerator, this.Sender);
            }

            internal AccountCreatedEventHandler Sut { get; }
        }
    }
}
