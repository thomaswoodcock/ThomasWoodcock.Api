using System;
using System.Threading.Tasks;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

using ThomasWoodcock.Service.Application.Accounts.Commands;
using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Application.Accounts.EventHandlers;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Domain.Accounts.DomainEvents;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Accounts.EventHandlers
{
    public sealed class AccountActivatedEventHandlerTests
    {
        public sealed class HandleAsync
        {
            [Fact]
            public async Task NoActivationKey_HandleAsync_DoesNotRemoveKeyFromRepository()
            {
                // Arrange
                Account account = Account.Create(new Guid("2BD822B1-0F99-4981-9DA4-9B4A6B7CC751"), "Test Name",
                        "test@test.com", "TestPassword123")
                    .Value ?? throw new InvalidOperationException();

                var keyRepository = Substitute.For<IAccountActivationKeyRepository>();

                keyRepository.GetAsync(account ?? throw new InvalidOperationException())
                    .ReturnsNull();

                AccountActivatedEventHandler sut = new(keyRepository);

                // Act
                await sut.HandleAsync(new AccountActivatedEvent(account));

                // Assert
                keyRepository.DidNotReceive()
                    .Remove(Arg.Any<AccountActivationKey>());

                await keyRepository.DidNotReceive()
                    .SaveAsync();
            }

            [Fact]
            public async Task ValidEvent_HandleAsync_RemovesKeyFromRepository()
            {
                // Arrange
                Account account = Account.Create(new Guid("2BD822B1-0F99-4981-9DA4-9B4A6B7CC751"), "Test Name",
                        "test@test.com", "TestPassword123")
                    .Value ?? throw new InvalidOperationException();

                AccountActivationKey key = new(new Guid("53F780F8-8447-4A9A-9620-8ECA936F6C27"));

                var keyRepository = Substitute.For<IAccountActivationKeyRepository>();

                keyRepository.GetAsync(account ?? throw new InvalidOperationException())
                    .Returns(key);

                AccountActivatedEventHandler sut = new(keyRepository);

                // Act
                await sut.HandleAsync(new AccountActivatedEvent(account));

                // Assert
                keyRepository.Received(1)
                    .Remove(key);

                await keyRepository.Received(1)
                    .SaveAsync();
            }
        }
    }
}
