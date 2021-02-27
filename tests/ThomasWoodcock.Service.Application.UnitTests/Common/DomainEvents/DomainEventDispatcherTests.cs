using System;
using System.Linq;
using System.Threading.Tasks;

using NSubstitute;

using ThomasWoodcock.Service.Application.Common.DomainEvents;
using ThomasWoodcock.Service.Domain.SeedWork;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Common.DomainEvents
{
    public sealed class DomainEventDispatcherTests
    {
        public sealed class Constructor
        {
            [Fact]
            public void NullHandlers_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => new DomainEventDispatcher(null));
            }
        }

        public sealed class DispatchAsync
        {
            [Fact]
            public async Task NullEvents_DispatchAsync_ThrowsArgumentNullException()
            {
                // Arrange
                DomainEventDispatcher sut = new(Enumerable.Empty<IDomainEventHandler>());

                // Act Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => sut.DispatchAsync(null));
            }

            [Fact]
            public async Task EventWithNoHandlers_DispatchAsync_DoesNotDispatchEvent()
            {
                // Arrange
                IDomainEvent domainEvent = new DomainEventStub();
                var eventHandler = Substitute.For<IDomainEventHandler<TestEvent>>();
                DomainEventDispatcher sut = new(new IDomainEventHandler[] { eventHandler });

                // Act
                await sut.DispatchAsync(new[] { domainEvent });

                // Assert
                await eventHandler.DidNotReceive()
                    .HandleAsync(Arg.Any<IDomainEvent>());
            }

            [Fact]
            public async Task SingleValidEvent_DispatchAsync_DispatchesEventToRespectiveHandlers()
            {
                // Arrange
                IDomainEvent domainEvent = new DomainEventStub();

                var firstEventHandler = Substitute.For<IDomainEventHandler<DomainEventStub>>();
                var secondEventHandler = Substitute.For<IDomainEventHandler<DomainEventStub>>();
                var thirdEventHandler = Substitute.For<IDomainEventHandler<TestEvent>>();

                DomainEventDispatcher sut =
                    new(new IDomainEventHandler[] { firstEventHandler, secondEventHandler, thirdEventHandler });

                // Act
                await sut.DispatchAsync(new[] { domainEvent });

                // Assert
                await firstEventHandler.Received(1)
                    .HandleAsync(domainEvent);

                await secondEventHandler.Received(1)
                    .HandleAsync(domainEvent);

                await thirdEventHandler.DidNotReceive()
                    .HandleAsync(Arg.Any<IDomainEvent>());
            }

            [Fact]
            public async Task MultipleValidEvents_DispatchAsync_DispatchesEventsToRespectiveHandlers()
            {
                // Arrange
                IDomainEvent firstDomainEvent = new DomainEventStub();
                IDomainEvent secondDomainEvent = new DomainEventStub();
                IDomainEvent thirdDomainEvent = new TestEvent();

                var firstEventHandler = Substitute.For<IDomainEventHandler<DomainEventStub>>();
                var secondEventHandler = Substitute.For<IDomainEventHandler<DomainEventStub>>();
                var thirdEventHandler = Substitute.For<IDomainEventHandler<TestEvent>>();

                DomainEventDispatcher sut =
                    new(new IDomainEventHandler[] { firstEventHandler, secondEventHandler, thirdEventHandler });

                // Act
                await sut.DispatchAsync(new[] { firstDomainEvent, secondDomainEvent, thirdDomainEvent });

                // Assert
                await firstEventHandler.Received(1)
                    .HandleAsync(firstDomainEvent);

                await firstEventHandler.Received(1)
                    .HandleAsync(secondDomainEvent);

                await secondEventHandler.Received(1)
                    .HandleAsync(firstDomainEvent);

                await secondEventHandler.Received(1)
                    .HandleAsync(secondDomainEvent);

                await thirdEventHandler.Received(1)
                    .HandleAsync(thirdDomainEvent);
            }

            internal sealed class TestEvent : IDomainEvent
            {
            }
        }
    }
}
