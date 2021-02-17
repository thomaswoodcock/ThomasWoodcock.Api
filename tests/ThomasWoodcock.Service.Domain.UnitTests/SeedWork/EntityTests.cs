using System;

using ThomasWoodcock.Service.Domain.SeedWork;

using Xunit;

namespace ThomasWoodcock.Service.Domain.UnitTests.SeedWork
{
    public sealed class EntityTests
    {
        public sealed class Constructor : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public Constructor(Fixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public void EmptyId_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => new TestEntity(Guid.Empty));
            }

            [Fact]
            public void ValidId_Constructor_SetsId()
            {
                // Arrange Act
                TestEntity sut = new(this._fixture.EntityId);

                // Assert
                Assert.Equal(this._fixture.EntityId, sut.Id);
            }
        }

        public sealed class RaiseDomainEvent : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public RaiseDomainEvent(Fixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public void NullDomainEvent_RaiseDomainEvent_ThrowsArgumentNullException()
            {
                // Arrange
                TestEntity sut = new(this._fixture.EntityId);

                // Act Assert
                Assert.Throws<ArgumentNullException>(() => sut.Raise(null));
            }

            [Fact]
            public void DomainEvent_RaiseDomainEvent_AddsDomainEvent()
            {
                // Arrange
                TestEntity sut = new(this._fixture.EntityId);
                TestDomainEvent domainEvent = new();

                // Act
                sut.Raise(domainEvent);

                // Assert
                Assert.Contains(domainEvent, sut.DomainEvents);
            }

            private sealed class TestDomainEvent : IDomainEvent
            {
            }
        }

        public sealed class Fixture
        {
            internal readonly Guid EntityId = new("5A47A93A-09CC-4B3A-BA0F-259ABCA3D72B");
        }

        private sealed class TestEntity : Entity
        {
            internal TestEntity(Guid id)
                : base(id)
            {
            }

            internal void Raise(IDomainEvent domainEvent) => this.RaiseDomainEvent(domainEvent);
        }
    }
}
