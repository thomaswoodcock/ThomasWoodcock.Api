using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Infrastructure.Persistence.Accounts;

using Xunit;

namespace ThomasWoodcock.Service.Infrastructure.UnitTests.Persistence.Accounts
{
    public sealed class EfAccountActivationKeyRepositoryTests
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
            public async Task NullAccount_GetAsync_ThrowsArgumentNullException()
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountActivationKeyRepository sut = new(context);

                // Act Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetAsync(null));
            }

            [Fact]
            public async Task ValidAccount_GetAsync_ReturnsActivationKey()
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountActivationKeyRepository sut = new(context);

                // Act
                AccountActivationKey key = await sut.GetAsync(this._fixture.Account);

                // Assert
                Assert.Equal(this._fixture.ActivationKey.Value, key.Value);
            }

            [Fact]
            public async Task InvalidAccount_GetAsync_ReturnsNull()
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountActivationKeyRepository sut = new(context);

                Account account = Account.Create(new Guid("6B6DE9BB-75E9-4948-ABE6-D91B9CFC41E4"), "Test Name",
                        "test@test.com", "TestPassword123")
                    .Value;

                // Act
                AccountActivationKey key = await sut.GetAsync(account);

                // Assert
                Assert.Null(key);
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
            public void NullAccount_Add_ThrowsArgumentNullException()
            {
                // Arrange
                using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountActivationKeyRepository sut = new(context);

                // Act Assert
                Assert.Throws<ArgumentNullException>(() => sut.Add(null, this._fixture.ActivationKey));
            }

            [Fact]
            public void NullActivationKey_Add_ThrowsArgumentNullException()
            {
                // Arrange
                using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountActivationKeyRepository sut = new(context);

                // Act Assert
                Assert.Throws<ArgumentNullException>(() => sut.Add(this._fixture.Account, null));
            }

            [Fact]
            public async Task ValidAccountAndKey_Add_AddsKeyToRepository()
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountActivationKeyRepository sut = new(context);

                AccountActivationKey newKey = new(new Guid("08152C00-7C17-4576-ADA6-828BD665FFE5"));

                // Act
                sut.Add(this._fixture.SecondAccount, newKey);
                await sut.SaveAsync();

                // Assert
                AccountActivationKey existingKey =
                    await context.ActivationKeys.SingleAsync(key => key.Value == newKey.Value);

                Assert.Equal(newKey.Value, existingKey.Value);
            }
        }

        [Collection("EfAccountDatabaseCollection")]
        public sealed class Remove
        {
            private readonly EfAccountDatabaseFixture _fixture;

            public Remove(EfAccountDatabaseFixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public void NullActivationKey_Remove_ThrowsArgumentNullException()
            {
                // Arrange
                using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountActivationKeyRepository sut = new(context);

                // Act Assert
                Assert.Throws<ArgumentNullException>(() => sut.Remove(null));
            }

            [Fact]
            public async Task ValidActivationKey_Remove_RemovesKeyFromRepository()
            {
                // Arrange
                await using AccountContext context = new(this._fixture.ContextOptions);
                EfAccountActivationKeyRepository sut = new(context);

                // Act
                sut.Remove(this._fixture.ActivationKeyToBeDeleted);
                await sut.SaveAsync();

                // Assert
                AccountActivationKey existingKey =
                    await context.ActivationKeys.SingleOrDefaultAsync(key =>
                        key.Value == this._fixture.ActivationKeyToBeDeleted.Value);

                Assert.Null(existingKey);
            }
        }
    }
}
