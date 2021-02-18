using System;

using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Domain.Accounts.DomainEvents;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

using Xunit;

namespace ThomasWoodcock.Service.Domain.UnitTests.Accounts.DomainEvents
{
    public sealed class AccountActivatedEventTests
    {
        public sealed class Constructor
        {
            [Fact]
            public void NullAccount_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => new AccountActivatedEvent(null));
            }

            [Fact]
            public void ValidAccount_Constructor_SetsAccount()
            {
                // Arrange
                IResult<Account> account = Account.Create(new Guid("1B403843-A4FE-482F-AC39-19DBD8FD9528"), "Test Name",
                    "test@test.com", "TestPassword123");

                // Act
                AccountActivatedEvent sut = new(account.Value);

                // Assert
                Assert.Equal(account.Value, sut.Account);
            }
        }
    }
}
