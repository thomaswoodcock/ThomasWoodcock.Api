using System;

using ThomasWoodcock.Service.Application.Accounts.Entities;
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
            public void NullActivationKey_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() =>
                    new AccountActivationNotification(this._fixture.Account, null));
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
            internal readonly AccountActivationKey
                ActivationKey = new(new Guid("DF2578E4-12C7-493C-A6E4-B2F7D3241763"));

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
