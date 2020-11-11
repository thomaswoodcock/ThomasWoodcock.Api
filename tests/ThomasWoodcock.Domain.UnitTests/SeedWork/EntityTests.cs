using System;

using ThomasWoodcock.Domain.SeedWork;

using Xunit;

namespace ThomasWoodcock.Domain.UnitTests.SeedWork
{
    public class EntityTests
    {
        public class ConstructorTests
        {
            [Fact]
            public void ValidArguments_Constructor_ReturnsEntity()
            {
                // Arrange
                var id = Guid.NewGuid();

                // Act
                TestEntity sut = new(id);

                // Assert
                Assert.IsAssignableFrom<Entity>(sut);
                Assert.Equal(id, sut.Id);
            }
        }

        public class EqualOperatorTests
        {
            [Fact]
            public void TwoNullEntities_EqualOperator_ReturnsTrue()
            {
                // Arrange
                Entity objectOne = null;
                Entity objectTwo = null;

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void TwoEntitiesWithSameId_EqualOperator_ReturnsTrue()
            {
                // Arrange
                var id = Guid.NewGuid();
                TestEntity objectOne = new(id);
                TestEntity objectTwo = new(id);

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void TwoEntitiesWithDifferentIds_EqualOperator_ReturnsFalse()
            {
                // Arrange
                TestEntity objectOne = new(new Guid("dcb1a0ab-ea79-482a-9cf5-621561b514f2"));
                TestEntity objectTwo = new(new Guid("5feda89d-640c-449a-b836-c2c66482a6e2"));

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void NullLeftOperand_EqualOperator_ReturnsFalse()
            {
                // Arrange
                TestEntity objectOne = null;
                TestEntity objectTwo = new(Guid.NewGuid());

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void NullRightOperand_EqualOperator_ReturnsFalse()
            {
                // Arrange
                TestEntity objectOne = new(Guid.NewGuid());
                TestEntity objectTwo = null;

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void ObjectOfDifferentType_EqualOperator_ReturnsFalse()
            {
                // Arrange
                TestEntity objectOne = new(Guid.NewGuid());
                AnotherTestEntity objectTwo = new(Guid.NewGuid());

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.False(result);
            }
        }

        public class NotEqualOperatorTests
        {
            [Fact]
            public void TwoNullEntities_NotEqualOperator_ReturnsFalse()
            {
                // Arrange
                Entity objectOne = null;
                Entity objectTwo = null;

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void TwoEntitiesWithSameId_NotEqualOperator_ReturnsFalse()
            {
                // Arrange
                var id = Guid.NewGuid();
                TestEntity objectOne = new(id);
                TestEntity objectTwo = new(id);

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void TwoEntitiesWithDifferentIds_NotEqualOperator_ReturnsTrue()
            {
                // Arrange
                TestEntity objectOne = new(new Guid("dcb1a0ab-ea79-482a-9cf5-621561b514f2"));
                TestEntity objectTwo = new(new Guid("5feda89d-640c-449a-b836-c2c66482a6e2"));

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void NullLeftOperand_NotEqualOperator_ReturnsTrue()
            {
                // Arrange
                TestEntity objectOne = null;
                TestEntity objectTwo = new(Guid.NewGuid());

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void NullRightOperand_NotEqualOperator_ReturnsTrue()
            {
                // Arrange
                TestEntity objectOne = new(Guid.NewGuid());
                TestEntity objectTwo = null;

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void ObjectOfDifferentType_NotEqualOperator_ReturnsTrue()
            {
                // Arrange
                TestEntity objectOne = new(Guid.NewGuid());
                AnotherTestEntity objectTwo = new(Guid.NewGuid());

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.True(result);
            }
        }

        public class EqualsTests
        {
            [Fact]
            public void NullObject_Equals_ReturnsFalse()
            {
                // Arrange
                TestEntity entity = new(Guid.NewGuid());

                // Act
                bool result = entity.Equals(null);

                // Assert
                Assert.False(result);
            }
        }

        public class GetHashCodeTests
        {
            [Fact]
            public void TwoEntitiesWithSameTypeAndId_GetHashCode_ReturnsSameHashCode()
            {
                // Arrange
                var id = Guid.NewGuid();
                TestEntity objectOne = new(id);
                TestEntity objectTwo = new(id);

                // Act
                int hashCodeOne = objectOne.GetHashCode();
                int hashCodeTwo = objectTwo.GetHashCode();

                // Assert
                Assert.Equal(hashCodeOne, hashCodeTwo);
            }

            [Fact]
            public void TwoEntitiesWithSameTypeAndDifferentIds_GetHashCode_ReturnsDifferentHashCodes()
            {
                // Arrange
                TestEntity objectOne = new(Guid.NewGuid());
                TestEntity objectTwo = new(Guid.NewGuid());

                // Act
                int hashCodeOne = objectOne.GetHashCode();
                int hashCodeTwo = objectTwo.GetHashCode();

                // Assert
                Assert.NotEqual(hashCodeOne, hashCodeTwo);
            }

            [Fact]
            public void TwoEntitiesWithDifferentTypeAndSameId_GetHashCode_ReturnsDifferentHashCode()
            {
                // Arrange
                var id = Guid.NewGuid();
                TestEntity objectOne = new(id);
                AnotherTestEntity objectTwo = new(id);

                // Act
                int hashCodeOne = objectOne.GetHashCode();
                int hashCodeTwo = objectTwo.GetHashCode();

                // Assert
                Assert.NotEqual(hashCodeOne, hashCodeTwo);
            }

            [Fact]
            public void TwoEntitiesWithDifferentTypeAndDifferentId_GetHashCode_ReturnsDifferentHashCode()
            {
                // Arrange
                TestEntity objectOne = new(Guid.NewGuid());
                AnotherTestEntity objectTwo = new(Guid.NewGuid());

                // Act
                int hashCodeOne = objectOne.GetHashCode();
                int hashCodeTwo = objectTwo.GetHashCode();

                // Assert
                Assert.NotEqual(hashCodeOne, hashCodeTwo);
            }
        }

        public class ToStringTests
        {
            [Fact]
            public void NoArguments_ToString_ReturnsExpectedString()
            {
                // Arrange
                TestEntity entity = new(new Guid("dcb1a0ab-ea79-482a-9cf5-621561b514f2"));

                // Act
                string result = entity.ToString();

                // Assert
                Assert.Equal("TestEntity [Id=dcb1a0ab-ea79-482a-9cf5-621561b514f2]", result);
            }
        }

        private class TestEntity : Entity
        {
            public TestEntity(Guid id)
                : base(id) { }
        }

        private class AnotherTestEntity : Entity
        {
            public AnotherTestEntity(Guid id)
                : base(id) { }
        }
    }
}
