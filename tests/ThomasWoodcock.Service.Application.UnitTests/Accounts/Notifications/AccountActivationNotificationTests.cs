using System;

using ThomasWoodcock.Service.Application.Accounts.Notifications;
using ThomasWoodcock.Service.Domain.Accounts;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Accounts.Notifications
{
    public sealed class AccountActivationNotificationTests
    {
        public sealed class Constructor : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public Constructor(Fixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public void NullAccount_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() =>
                    new AccountActivationNotification(null, this._fixture.ActivationKey));
            }

            [Fact]
            public void EmptyActivationKey_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() =>
                    new AccountActivationNotification(this._fixture.Account, Guid.Empty));
            }

            [Fact]
            public void ValidArguments_Constructor_SetsProperties()
            {
                // Arrange Act
                AccountActivationNotification sut = new(this._fixture.Account, this._fixture.ActivationKey);

                // Assert
                Assert.Equal(this._fixture.Account, sut.Account);
                Assert.Equal(this._fixture.ActivationKey, sut.ActivationKey);
            }
        }

        public sealed class Fixture
        {
            internal readonly Guid ActivationKey = new("A03008CF-AAF4-43A6-84D6-DA5EBC7E5072");

            public Fixture()
            {
                this.Account = Account.Create(new Guid("F19C0F3D-D471-47B7-A7BE-4416AED57B8E"), "Test Name",
                        "test@test.com", "TestPassword123")
                    .Value;
            }

            internal Account Account { get; }
        }
    }
}
