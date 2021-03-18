using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Infrastructure.Persistence.Accounts;

using Xunit;

namespace ThomasWoodcock.Service.Infrastructure.UnitTests.Persistence.Accounts
{
    public sealed class EfAccountCommandRepositoryTests
    {
        [Collection("EfAccountDatabaseCollection")]
        public sealed class GetAsync
        {
            private readonly EfAccountDatabaseFixture _fixture;

            public GetAsync(EfAccountDatabaseFixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public async Task EmptyAccountId_GetAsync_ThrowsArgumentNullException()
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountCommandRepository sut = new(context);

                // Act Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetAsync(Guid.Empty));
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public async Task NullEmptyOrWhiteSpaceEmailAddress_GetAsync_ThrowsArgumentNullException(
                string emailAddress)
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountCommandRepository sut = new(context);

                // Act Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetAsync(emailAddress));
            }

            [Fact]
            public async Task ValidAccountId_GetAsync_ReturnsAccount()
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountCommandRepository sut = new(context);

                // Act
                Account account = await sut.GetAsync(this._fixture.Account.Id) ?? throw new InvalidOperationException();

                // Assert
                Assert.Equal(this._fixture.Account.Id, account.Id);
                Assert.Equal(this._fixture.Account.Name, account.Name);
                Assert.Equal(this._fixture.Account.EmailAddress.ToString(), account.EmailAddress.ToString());
                Assert.Equal(this._fixture.Account.Password, account.Password);
                Assert.Equal(this._fixture.Account.IsActive, account.IsActive);
            }

            [Fact]
            public async Task InvalidAccountId_GetAsync_ReturnsNull()
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountCommandRepository sut = new(context);

                // Act
                Account? account = await sut.GetAsync(new Guid("4D492A26-3DC8-45D6-828B-0917DCC18213"));

                // Assert
                Assert.Null(account);
            }

            [Fact]
            public async Task ValidEmailAddress_GetAsync_ReturnsAccount()
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountCommandRepository sut = new(context);

                // Act
                Account account = await sut.GetAsync(this._fixture.Account.EmailAddress.ToString()) ??
                                  throw new InvalidOperationException();

                // Assert
                Assert.Equal(this._fixture.Account.Id, account.Id);
                Assert.Equal(this._fixture.Account.Name, account.Name);
                Assert.Equal(this._fixture.Account.EmailAddress.ToString(), account.EmailAddress.ToString());
                Assert.Equal(this._fixture.Account.Password, account.Password);
                Assert.Equal(this._fixture.Account.IsActive, account.IsActive);
            }

            [Fact]
            public async Task InvalidEmailAddress_GetAsync_ReturnsNull()
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountCommandRepository sut = new(context);

                // Act
                Account? account = await sut.GetAsync("fake@fake.com");

                // Assert
                Assert.Null(account);
            }
        }

        [Collection("EfAccountDatabaseCollection")]
        public sealed class Add
        {
            private readonly EfAccountDatabaseFixture _fixture;

            public Add(EfAccountDatabaseFixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public async Task ValidAccount_Add_AddsAccountToRepository()
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountCommandRepository sut = new(context);

                Account newAccount = Account.Create(new Guid("6B6DE9BB-75E9-4948-ABE6-D91B9CFC41E4"), "Test Name",
                        "test@test.com", "TestPassword123")
                    .Value ?? throw new InvalidOperationException();

                // Act
                sut.Add(newAccount);
                await sut.SaveAsync();

                // Assert
                Account existingAccount = await context.Accounts.SingleAsync(account => account.Id == newAccount.Id);

                Assert.Equal(newAccount.Id, existingAccount.Id);
                Assert.Equal(newAccount.Name, existingAccount.Name);
                Assert.Equal(newAccount.EmailAddress.ToString(), existingAccount.EmailAddress.ToString());
                Assert.Equal(newAccount.Password, existingAccount.Password);
                Assert.Equal(newAccount.IsActive, existingAccount.IsActive);
            }
        }
    }
}
